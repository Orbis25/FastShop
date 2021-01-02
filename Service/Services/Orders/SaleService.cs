using BussinesLayer.Repository;
using DataLayer.Utils.Paginations;
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
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Svc
{

    public class SaleService : BaseRepository<Sale, ApplicationDbContext, Guid>, ISaleService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSetting _settings;
        private readonly InternalConfiguration _internalOptions;
        private readonly IOrderService _order;
        public SaleService(ApplicationDbContext context,
            IOptions<EmailSetting> options,
            IOrderService order,
            IOptions<InternalConfiguration> internalOptions)
            : base(context)
        {
            _context = context;
            _settings = options.Value;
            _order = order;
            _internalOptions = internalOptions.Value;
        }
        public override async Task<bool> Add(Sale model)
        {
            var order = new Order() { };
            var result = await _order.Add(order);
            if (!result) return false;
            model.OrderId = order.Id;
            model.CreatedAt = DateTime.Now;
            return await base.Add(model);
        }

        public async Task<bool> CreateSale(Sale sale, string userEmail)
        {
            if (await Add(sale))
            {
                sale.DetailSales.ForEach(_ =>
                {
                    _.CreatedAt = DateTime.Now;
                    _.SaleId = sale.Id;
                });
                if (await UpdateProducts(sale.DetailSales))
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

        public override async Task<Sale> GetById(Guid id, params Expression<Func<Sale, object>>[] includes)
        {
            var model = await _context.Sales.Include(x => x.User).Include(x => x.Order).Include(x => x.DetailSales)
                .ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
            return model;
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
            { }
            return true;
        }

        private static async Task<string> ReadTemplateEmailAsString() => await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\email", "Template.html"));
    }
}
