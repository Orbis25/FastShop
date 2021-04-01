using DataLayer.Utils.Paginations;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) => _context = context;

        public async Task<ApplicationUser> Get(string id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<PaginationResult<ApplicationUser>> GetAllPaginated(PaginationBase pagination)
        {
            var result = _context.Users.AsQueryable();
            int total = result.Count();
            var pages = total / pagination.Qyt;
            result = result.Skip((pagination.Page - 1) * pagination.Qyt).Take(pagination.Qyt).OrderByDescending(x => x.UserName);
            return new()
            {
                ActualPage = pagination.Page,
                Qyt = pagination.Qyt,
                Pages = pages,
                Total = total,
                Results = await result.ToListAsync()
            };
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers() => await _context.Users.ToListAsync();
    }
}
