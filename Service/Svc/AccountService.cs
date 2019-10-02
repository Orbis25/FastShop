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
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        public AccountService(ApplicationDbContext context) => _context = context;

        public async Task<bool> BlockAndUnlockAccount(Guid id)
        {
            try
            {
                var model = _context.ApplicationUsers.SingleOrDefault(x => x.Id == id.ToString());
                model.LockoutEnabled = (model.LockoutEnabled == (false)) ? true : false;
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            var model = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Email.Equals(email));
            if (model != null) return model;
            return null;
        }
    }
}
