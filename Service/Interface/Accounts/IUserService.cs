using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Accounts;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<PaginationResult<ApplicationUser>> GetAllPaginated(UserFilterVM userFilter, Expression<Func<ApplicationUser,bool>> expression = null);
        Task<ApplicationUser> Get(string id);
        Task<bool> Update(ApplicationUser model);
    }
}
