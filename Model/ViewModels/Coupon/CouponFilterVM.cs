using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Coupon
{
    public class CouponFilterVM
    {
        public string Q { get; set; }
        public StateCuppon? State { get; set; } = null;
    }
}
