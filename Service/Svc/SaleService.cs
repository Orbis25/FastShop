using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Models;
using Model.Settings;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Svc
{

    public class SaleService : ISaleService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSetting _settings;
        public SaleService(ApplicationDbContext context , IOptions<EmailSetting> options)
        {
            _context = context;
            _settings = options.Value;
        }
        public async Task<bool> Add(Sale model)
        {
            model.CreatedAt = DateTime.Now;
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateSale(Sale sale, string userEmail)
        {
            if(await Add(sale))
            {
                sale.DetailSales.ForEach(_ =>
                {
                    _.CreatedAt = DateTime.Now;
                    _.SaleId = sale.Id;
                });
               if(await UpdateProducts(sale.DetailSales))
               {
                    await SendEmail(sale, userEmail);
                    if (!await AddDetailSale(sale.DetailSales)) return false;
               }
            }
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> AddDetailSale(IEnumerable<DetailSale> model)
        {
            _context.DetailSales.AddRange(model);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        private async Task<bool> UpdateProducts(IEnumerable<DetailSale> model)
        {
            try
            {
                foreach (var item in model)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                    product.Quantity -= item.Quantity;
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<IEnumerable<Sale>> GetAll() => await _context.Sales.ToListAsync();

        public async Task<Sale> GetById(Guid id)
        {
            var model = await _context.Sales.Include(x => x.DetailSales).Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);
            model.User = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Id == model.ApplicationUserId);
            return model;
        }

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                var model = await GetById(id);
                model.UpdatedAt = DateTime.Now;
                model.State = Model.Enums.State.deleted;
                _context.Update(model);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Sale model)
        {
            try
            {
                model.UpdatedAt = DateTime.Now;
                _context.Update(model);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> SendEmail(Sale model, string userEmail)
        {

            var html = await ReadTemplateEmailAsString();
            html = html.Replace("{date}", model.CreatedAt.ToString());
            html = html.Replace("{user}", userEmail);
            html = html.Replace("{cuppon}", !string.IsNullOrEmpty(model.CuponCode) ? model.CuponCode : "---");
            html = html.Replace("{total}", model.Total.ToString());
            string table = string.Empty;
            foreach (var item in model.DetailSales)
            {
                var prd = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                table += $"<tr><td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>{prd.ProductName}</td>" +
                    $"<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>{item.Quantity}</td>" +
                    $"<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;' >{prd.Price}</td></tr>";
            }
            html = html.Replace("{body}", table);


            var smtp = new SmtpClient()
            {
                Host = _settings.Smtp,
                EnableSsl = true,
                UseDefaultCredentials = _settings.DefaultCredentials,
                Credentials = new NetworkCredential(_settings.User, _settings.Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_settings.User)
            };
            mailMessage.To.Add(userEmail);
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "FastShop --- Bill";
            mailMessage.Body = html;
            try
            {
                await smtp.SendMailAsync(mailMessage);
            }
            catch
            {}
            return true;
        }

        private async Task<string> ReadTemplateEmailAsString() => await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\email", "Template.html"));
    }
}
