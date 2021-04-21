using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Accounts;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context) => _context = context;

        public async Task<ApplicationUser> Get(string id) => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<PaginationResult<ApplicationUser>> GetAllPaginated(UserFilterVM userFilter, Expression<Func<ApplicationUser, bool>> expression = null)
        {
            var result = expression == null ? _context.Users.AsQueryable() : _context.Users.Where(expression);
            if (!string.IsNullOrEmpty(userFilter.FullName)) result = result.Where(x => x.FullName.Contains(userFilter.FullName));
            if (userFilter.IsLocked.HasValue) result = result.Where(x => x.LockoutEnabled == userFilter.IsLocked.Value);
            int total = result.Count();
            var pages = total / userFilter.Qyt;
            result = result.Skip((userFilter.Page - 1) * userFilter.Qyt)
                .Take(userFilter.Qyt).OrderByDescending(x => x.UserName);
            return new()
            {
                ActualPage = userFilter.Page,
                Qyt = userFilter.Qyt,
                Pages = pages,
                Total = total,
                Results = await result.ToListAsync()
            };
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers() => await _context.Users.ToListAsync();
    }
}
