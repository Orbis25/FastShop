using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Product>> GetAll() => await _context.Products.ToListAsync();

        public async Task<Product> GetById(Guid id) => await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Remove(Guid id)
        {
            try
            {
                var model = await GetById(id);
                model.UpdatedAt = DateTime.Now;
                model.State = Model.Enums.State.deleted;
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
                var product = await GetById(model.Id);
                product.Price = model.Price;
                product.Model = model.Model;
                product.ProductName = model.ProductName;
                product.UpdatedAt = DateTime.Now;
                _context.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
