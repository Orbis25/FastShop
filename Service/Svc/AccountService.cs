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
                var model = _context.ApplicationUsers.FirstOrDefault(x => x.Id.Equals(id));
                model.LockoutEnabled = false;// model.LockoutEnabled == (false) ? true : false;
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
