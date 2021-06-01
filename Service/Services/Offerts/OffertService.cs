using BussinesLayer.Repository;
using DataLayer.Utils.Paginations;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System.Linq;
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

        public async Task<PaginationResult<Offert>> filter(PaginationBase pagination, string q)
        {
            var result = await GetAllPaginated(pagination, (string.IsNullOrEmpty(q) ? null :  x => x.Concept.Contains(q)));
            if (!result.Results.Any()) result = await GetAllPaginated(pagination, x => x.Description.Contains(q));
            return result;
        }

        public async Task<Offert> GetActiveOffert() => await _context.Offerts.Include(x => x.ImageOfferts).FirstOrDefaultAsync(x => x.State != Model.Enums.State.Deleted);

        public async Task<Offert> GetById(int id) => await _context.Offerts.Include(x => x.ImageOfferts).SingleOrDefaultAsync(x => x.Id == id && x.State != Model.Enums.State.Deleted);

        public async  Task<bool> RemoveImage(int id)
        {
            var image = await _context.ImageOfferts.FindAsync(id);
            if (image == null) return false;
            _context.Remove(image);
            return await _context.SaveChangesAsync() > 0;
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
