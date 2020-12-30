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
        private readonly InternalConfiguration _internalOptions;
        private readonly IOrderService _order;
        public SaleService(ApplicationDbContext context , IOptions<EmailSetting> options , IOrderService order , IOptions<InternalConfiguration> internalOptions)
        {
            _context = context;
            _settings = options.Value;
            _order = order;
            _internalOptions = internalOptions.Value;
        }
        public async Task<bool> Add(Sale model)
        {
                var order = new Order() {  };
                if (await _order.Add(order))
                {
                    model.OrderId = order.Id;
                    model.CreatedAt = DateTime.Now;
                    await _context.AddAsync(model);
                }
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
            var model = await _context.Sales.Include(x => x.DetailSales).Include(x => x.Order).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return model; 
            model.User = (model != null) ? await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == model.ApplicationUserId) : new ApplicationUser();
            return model;
        }

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                var model = await GetById(id);
                model.UpdatedAt = DateTime.Now;
                model.State = Model.Enums.State.Deleted;
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
            html = html.Replace("{cuppon}", !string.IsNullOrEmpty(model.CuponCode) ? model.CuponCode : "N/A");
            html = html.Replace("{total}", model.Total.ToString());
            html = html.Replace("{code}", model.Code);
            html = html.Replace("{orderCode}", model.Order.OrderCode);
            html = html.Replace("{paymentType}", model.PaymentType.ToString());
            string table = string.Empty;
            foreach (var item in model.DetailSales)
            {
                var prd = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                table += $"<tr><td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>{prd.ProductName}</td>" +
                    $"<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>{item.Quantity}</td>" +
                    $"<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;' >{prd.Price}</td></tr>";
            }
            html = html.Replace("{body}", table);
            html = html.Replace("{AppName}", _internalOptions.AppName);




            var smtp = new SmtpClient()
            {
                Host = _settings.Smtp,
                EnableSsl = true,
                UseDefaultCredentials = _settings.DefaultCredentials,
                Credentials = new NetworkCredential(_settings.User, _settings.Password)
            };
         
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.User)
                };
                mailMessage.To.Add(userEmail);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = $"{_internalOptions.AppName} --- Bill";
                mailMessage.Body = html;

                await smtp.SendMailAsync(mailMessage);
            }
            catch
            {}
            return true;
        }

        private static async Task<string> ReadTemplateEmailAsString() => await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\email", "Template.html"));
    }
}
