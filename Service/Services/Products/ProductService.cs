using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.ViewModels;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context) => _context = context;
        
        public async Task<bool> Add(Product model)
        {
            try
            {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<IEnumerable<Product>> GetAll() => await _context.Products.Include(x => x.Category).ToListAsync();

        public async Task<Product> GetById(Guid id) => await _context.Products.Include(x => x.Category).Include(x => x.ProductPics).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                var model = await GetById(id);
                model.UpdatedAt = DateTime.Now;
                model.State = Model.Enums.State.Deleted;
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Product model)
        {
            try
            {
                model.UpdatedAt = DateTime.Now;
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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

        public async Task<ProductPaginationVM> GetAllPaginateProducts(int take = 9, int page = 1)
        {
            var model =  await _context.Products.OrderBy(x => x.CreatedAt)
                .Skip((page - 1) * take)
                .Take(take)
                .Include(x => x.ProductPics)
                .Include(x => x.Category)
                .Where(x => x.Quantity > 0).ToListAsync();

            return new ProductPaginationVM
            {
                Products = model,
                ActualPage = page,
                TotalOfRegisters = _context.Products.Count(),
                RegisterByPage = take
            };

        }

        public async Task<ProductPaginationVM> Filter(Filter filter)
        {
            var model = _context.Products.AsQueryable();
            model = !string.IsNullOrEmpty(filter.Parameter) ?
                    model.Where(x => x.ProductName.Contains(filter.Parameter)) : model;
            model = (filter.From > 0 && filter.To > 0) ? model.Where(x => x.Price >= filter.From && x.Price <= filter.To) : model;
            model = (filter.Category != null) ? model.Where(x => x.CategoryId == filter.Category) : model;
            model = model.Take(filter.Take).Skip((filter.Index - 1) * filter.Take);


            return new ProductPaginationVM {
                ActualPage = filter.Index,
                RegisterByPage = filter.Take,
                TotalOfRegisters = model.Count(),
                Products = await model.Include(x => x.Category).Include(x => x.ProductPics).Where(x => x.Quantity > 0).ToListAsync()
            };

        }
    }
}
