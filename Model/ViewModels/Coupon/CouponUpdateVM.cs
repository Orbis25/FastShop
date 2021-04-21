using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Coupon
{
    public class CouponUpdateVM
    {
        public int Id { get; set; }
        public StateCuppon State { get; set; }
    }
}
