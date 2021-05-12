using BussinesLayer.Repository;
using Commons.Helpers;
using DataLayer.Enums.Base;
using DataLayer.Models.Cart;
using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Orders;
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
using System.Threading.Tasks;

namespace Service.Svc
{

    public class SaleService : BaseRepository<Sale, ApplicationDbContext, Guid>, ISaleService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSetting _settings;
        private readonly InternalConfiguration _internalOptions;
        public SaleService(ApplicationDbContext context,
            IOptions<EmailSetting> options,
            IOptions<InternalConfiguration> internalOptions)
            : base(context)
        {
            _context = context;
            _settings = options.Value;
            _internalOptions = internalOptions.Value;
        }
        public override async Task<bool> Add(Sale model)
        {
            //create the order
            var order = new Order() { };
            await _context.Orders.AddAsync(order);
            var result = await CommitAsync();
            if (!result) return false;
            model.OrderId = order.Id;
            return await base.Add(model);
        }

        public async Task<Sale> CreateSale(List<CartItem> items, Sale sale, string userEmail)
        {
            ///TODO: OBTIMIZAR ESTO PORQUE PUEDEN AGREGARSE A LA DB SIN HACER FOR_EARCH
            var itemsToSale = new List<DetailSale>();
            items.ForEach(item =>
            {
                itemsToSale.Add(new()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
            });

            var total = items.Sum(x => (x.Quantity * x.Product.Price));
            var coupon = await _context.Cupons.FirstOrDefaultAsync(x => x.Code == sale.Code);

            if (coupon != null)
            {
                if (coupon.IsByPercent)
                {
                    total = (total * coupon.Amount) / 100;
                }
                else
                {
                    total -= coupon.Amount;
                }
            }

            sale.Total = total;
            var result = await Add(sale);
            if (!result) return null;
            if (await UpdateProducts(itemsToSale))
            {
                sale.DetailSales = itemsToSale;
                if (!await AddDetailSale(itemsToSale)) return null;
            }

            return sale;
        }

        private async Task<bool> AddDetailSale(IEnumerable<DetailSale> model)
        {
            _context.DetailSales.AddRange(model);
            return await CommitAsync();
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
                    await CommitAsync();
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

        public async Task<string> GetTemplateEmail(Sale model, string userEmail)
        {

            var template = await _context.EmailTemplates.FirstOrDefaultAsync(x => x.Type == TemplateTypeEnum.Bill);
            var html = template.Body;
            html = html.Replace("{date}", model.CreatedAtSrt);
            html = html.Replace("{user}", userEmail);
            html = html.Replace("{cuppon}", !string.IsNullOrEmpty(model.CuponCode) ? model.CuponCode : "N/A");
            html = html.Replace("{total}", model.Total.ToString("C"));
            html = html.Replace("{code}", model.Code);
            html = html.Replace("{orderCode}", model.Order.OrderCode);
            html = html.Replace("{paymentType}", model.PaymentType.ToString());
            string table = string.Empty;
            foreach (var item in model.DetailSales)
            {
                var prd = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                table += $"<tr><td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>{prd.ProductName}</td>" +
                    $"<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>{item.Quantity}</td>" +
                    $"<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;' >{prd.Price.ToString("C")}</td></tr>";
            }
            html = html.Replace("{body}", table);
            html = html.Replace("{AppName}", _internalOptions.AppName);

            return html;
        }


        public async Task<SaleFilterVM> GetSales(SaleFilterVM filters)
        {
            var result = GetAll(null, x => x.Order, x => x.User);
            if (!string.IsNullOrEmpty(filters.Param))
            {
                result = result.Where(x => x.Order.OrderCode.Contains(filters.Param)
                || x.User.FullName.Contains(filters.Param)
                || x.Total.ToString().Contains(filters.Param));
            }
            if (!string.IsNullOrEmpty(filters.DateFrom) && !string.IsNullOrEmpty(filters.DateTo))
            {
                result = result.Where(x => x.CreatedAt >= filters.DateFrom.ToDate()
                         && x.CreatedAt <= filters.DateTo.ToDate());
            }

            if (filters.State.HasValue) result = result.Where(x => x.Order.StateOrder == filters.State);

            var total = result.Count();

            var pages = total / filters.Qyt;
            result = result.Skip((filters.Page - 1) * filters.Qyt).Take(filters.Qyt);

            return new()
            {
                Pages = pages,
                Total = total,
                Qyt = filters.Qyt,
                Page = filters.Page,
                Sales = await result.ToListAsync()
            };

        }

        public async Task<PaginationResult<Sale>> GetPurcharseHistory(PaginationBase pagination, string userId)
        {
            var list = GetAll(x => x.ApplicationUserId == userId, x => x.Order);
            list = list
                .Include(x => x.Order)
                .Include(x => x.DetailSales)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.ProductPics).AsNoTracking();
            return await CreatePagination(pagination, list);
        }

        public async Task<bool> CantReview(Guid productId, string userId)
        {
            var result = await _context.Sales
                                .Include(x => x.DetailSales).ToListAsync();
            return result.Any(x => x.ApplicationUserId == userId && x.DetailSales.Any(x => x.ProductId == productId));
        }


    }
}
