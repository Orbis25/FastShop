using DataLayer.Utils.Paginations;
using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Orders
{
    public class SaleFilterVM : PaginationBase
    {
        public string Param { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public StateOrder? State { get; set; } = null;
        public IEnumerable<Sale> Sales { get; set; }
    }
}
