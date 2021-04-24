using BussinesLayer.Repository;
using DataLayer.Enums.Products;
using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
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
                .Where(x => x.Quantity > (int)ProductStatusEnum.SoldOut).AsQueryable();

            //get by name
            if (!string.IsNullOrEmpty(filters.Name)) results = results.Where(x => x.ProductName.Contains(filters.Name));
            //get by category
            if (filters.Category.HasValue && filters.Category.Value > 0) results = results.Where(x => x.CategoryId == filters.Category);
            //range of money
            if (filters.To > 0) results = results.Where(x => x.Price >= filters.From && x.Price <= filters.To);

            switch (filters.Status)
            {
                case ProductStatusEnum.Spent:
                    results = results.Where(x => x.Quantity <= (int)ProductStatusEnum.Spent);
                    break;
                case ProductStatusEnum.AlmostSpent:
                    results = results.Where(x => x.Quantity == (int)ProductStatusEnum.AlmostSpent);
                    break;
                case ProductStatusEnum.Good:
                    results = results.Where(x => x.Quantity >= (int)ProductStatusEnum.Good);
                    break;

          
            }

            return new ProductFilterVM
            {
                Results = await results.Skip((filters.Page - 1) * filters.Qyt).Take(filters.Qyt).ToListAsync(),
                Page = filters.Page,
                Total = results.Count(x => x.Category.State != State.Deleted),
                Qyt = filters.Qyt
            };

        }

        public async Task<bool> RemoveProductPic(int id)
        {
            var result = await _context.ProductPics.FindAsync(id);
            if (result == null) return false;
            _context.Remove(result);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
