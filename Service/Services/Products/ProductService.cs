using BussinesLayer.Repository;
using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.ViewModels;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class ProductService : BaseRepository<Product, ApplicationDbContext, Guid>, IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context) : base(context) => _context = context;


        public async Task<bool> UplodadPic(ProductPic model)
        {
            try
            {
                _context.ProductPics.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductPic>> ProductPics(Guid Id)
            => await _context.ProductPics.Where(x => x.ProductId.Equals(Id)).ToListAsync();

        public async Task<List<Product>> GetHomeProducts(int take = 8)
            => await _context.Products.Include(x => x.ProductPics)
            .Include(x => x.Category).Where(x => x.Quantity > 0).OrderBy(x => x.CreatedAt).Take(take).ToListAsync();

        public async Task<ProductFilterVM> GetAllPaginateProducts(ProductFilterVM filters)
        {
            var results =  _context.Products.OrderBy(x => x.CreatedAt)
                
                .Include(x => x.ProductPics)
                .Include(x => x.Category)
                .Where(x => x.Quantity > 0).AsQueryable();

            //get by name
            if (!string.IsNullOrEmpty(filters.Name)) results = results.Where(x => x.ProductName.Contains(filters.Name));
            //get by category
            if (filters.Category.HasValue && filters.Category.Value > 0) results = results.Where(x => x.CategoryId == filters.Category);
            //range of money
            if (filters.To > 0) results = results.Where(x => x.Price >= filters.From && x.Price <= filters.To);

            return new ProductFilterVM
            {
                Results = await results.Skip((filters.Page - 1) * filters.Qyt).Take(filters.Qyt).ToListAsync(),
                Page = filters.Page,
                Total = results.Count(),
                Qyt = filters.Qyt
            };

        }
 
    }
}
