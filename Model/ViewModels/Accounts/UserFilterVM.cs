using DataLayer.Utils.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Accounts
{
    public class UserFilterVM :  PaginationBase
    {
        public string FullName { get; set; }
        public bool? IsLocked { get; set; }
    }
}
