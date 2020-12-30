using BussinesLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class CouponService : BaseRepository<Cupon, ApplicationDbContext, int>, ICouponService
    {
        private readonly ApplicationDbContext _context;
        public CouponService(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<Cupon> GetByCupponCode(string code) => await _context.Cupons.FirstOrDefaultAsync(x => x.Code.Equals(code));
        
    }
}
