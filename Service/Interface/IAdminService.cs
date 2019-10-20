using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAdminService
    {
        IEnumerable<DashboardVM> GetAllCountServices();
    }
}
