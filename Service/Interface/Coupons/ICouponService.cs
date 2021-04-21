using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Coupon;
using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICouponService  : IBaseRepository<Cupon , int>
    {
        Task<Cupon> GetByCupponCode(string code);
        Task<PaginationResult<Cupon>> GetFiltered(PaginationBase pagination, CouponFilterVM filters);
        Task<bool> SetValidOrInvalid(CouponUpdateVM model);
    }
}
