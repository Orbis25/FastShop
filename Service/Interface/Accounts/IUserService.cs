using DataLayer.Utils.Paginations;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<PaginationResult<ApplicationUser>> GetAllPaginated(PaginationBase pagination);
        Task<ApplicationUser> Get(string id);
    }
}
