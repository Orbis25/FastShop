using BussinesLayer.Repository;
using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Coupon;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class CouponService : BaseRepository<Cupon, ApplicationDbContext, int>, ICouponService
    {
        private readonly ApplicationDbContext _context;
        public CouponService(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<Cupon> GetByCupponCode(string code) => await _context.Cupons.FirstOrDefaultAsync(x => x.Code.Equals(code));

        public async Task<PaginationResult<Cupon>> GetFiltered(PaginationBase pagination, CouponFilterVM filters)
        {
            var result = _context.Cupons.AsQueryable();
            if (!string.IsNullOrEmpty(filters.Q))
            {
                result = result.Where(x => x.Code.Contains(filters.Q) 
                        || x.Concept.Contains(filters.Q) 
                        || x.Amount.ToString().Contains(filters.Q));
                if (filters.State.HasValue) result = result.Where(x => x.StateOfCuppon == filters.State);
            }

            var total = result.Count(); 

            var pages = total / pagination.Qyt;
            result = result.Skip((pagination.Page - 1) * pagination.Qyt).Take(pagination.Qyt);

            return new()
            {
                Pages = pages,
                Total = total,
                Qyt = pagination.Qyt,
                ActualPage = pagination.Page,
                Results = await result.ToListAsync()
            };
        }

        public  async Task<bool> SetValidOrInvalid(CouponUpdateVM model)
        {
            var result = await GetById(model.Id);
            if (result == null) return false;
            result.StateOfCuppon = model.State;
            return await Update(result);
        }
    }
}
