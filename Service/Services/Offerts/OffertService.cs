using BussinesLayer.Repository;
using DataLayer.Utils.Paginations;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class OffertService : BaseRepository<Offert, ApplicationDbContext, int>, IOffertService
    {
        private readonly ApplicationDbContext _context;
        public OffertService(ApplicationDbContext context) : base(context) => _context = context;
        public override async Task<bool> Add(Offert model)
        {
            var list = await _context.Offerts.ToListAsync();
            list.ForEach(x => x.State = Model.Enums.State.Deleted);
            return await base.Add(model);

        }

        public async Task<Offert> GetActiveOffert() => await _context.Offerts.Include(x => x.ImageOfferts).FirstOrDefaultAsync(x => x.State != Model.Enums.State.Deleted);


        public async Task<Offert> GetById(int id) => await _context.Offerts.Include(x => x.ImageOfferts).SingleOrDefaultAsync(x => x.Id == id && x.State != Model.Enums.State.Deleted);


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
