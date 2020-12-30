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
    public class CouponService : ICouponService
    {
        private readonly ApplicationDbContext _context;
        public CouponService(ApplicationDbContext context) => _context = context;

        public async Task<bool> Add(Cupon model)
        {
            try
            {
                model.CreatedAt = DateTime.Now;
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Cupon>> GetAll() => await _context.Cupons.ToListAsync();

        public async Task<Cupon> GetById(int id) => await _context.Cupons.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Cupon> GetByCupponCode(string code)
        {
           return await _context.Cupons.FirstOrDefaultAsync(x => x.Code.Equals(code));
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                var model = await GetById(id);
                model.State = Model.Enums.State.Deleted;
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

        public async Task<bool> Update(Cupon model)
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
    }
}
