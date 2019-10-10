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
    public class SaleService : ISaleService
    {
        private readonly ApplicationDbContext _context;
        public SaleService(ApplicationDbContext context) => _context = context;
        public async Task<bool> Add(Sale model)
        {
            model.CreatedAt = DateTime.Now;
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Sale>> GetAll() => await _context.Sales.ToListAsync();

        public async Task<Sale> GetById(Guid id) => await _context.Sales.Include(x => x.DetailSales).SingleOrDefaultAsync();

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
    }
}
