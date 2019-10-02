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
    public class OffertService : IOffertService
    {
        private readonly ApplicationDbContext _context;
        public OffertService(ApplicationDbContext context) => _context = context;
        public async Task<bool> Add(Offert model)
        {
            try
            {
                 await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Offert>> GetAll() => await _context.Offerts.Include(x => x.ImageOfferts).ToListAsync();

        public async Task<Offert> GetById(int id) => await _context.Offerts.Include(x => x.ImageOfferts).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Remove(int id)
        {
            try
            {
                var model = await GetById(id);
                model.State = Model.Enums.State.deleted;
                model.UpdatedAt = DateTime.Now;
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Offert model)
        {
            try
            {
                model.UpdatedAt = DateTime.Now;
                _context.Offerts.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> UploadImg(ImageOffert model)
        {
            try
            {
                await _context.ImageOfferts.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch 
            {
            }
            return false;
        }
    }
}
