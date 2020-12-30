using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var list = await _context.Offerts.ToListAsync();
                 list.ForEach(x =>  
                 x.State = Model.Enums.State.Deleted
                );
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Offert> GetActiveOffert() => await _context.Offerts.Include(x => x.ImageOfferts).FirstOrDefaultAsync(x => x.State != Model.Enums.State.Deleted);

        public async Task<IEnumerable<Offert>> GetList() 
            => await _context.Offerts.Include(x => x.ImageOfferts)
            .OrderBy(x => x.State == Model.Enums.State.Active)
            .ToListAsync();

        public async Task<Offert> GetById(int id) => await _context.Offerts.Include(x => x.ImageOfferts).SingleOrDefaultAsync(x => x.Id == id && x.State != Model.Enums.State.Deleted);

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
