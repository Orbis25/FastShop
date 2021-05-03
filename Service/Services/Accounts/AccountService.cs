using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Models;
using Model.Settings;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSetting _options;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InternalConfiguration _internalOptions;
        public AccountService(ApplicationDbContext context,
            IOptions<EmailSetting> options,
            IOptions<InternalConfiguration> internalOptions,
            UserManager<ApplicationUser> userManager)
        { 
            _context = context;
            _options = options.Value;
            _internalOptions = internalOptions.Value;
            _userManager = userManager;
        }

        public async Task<bool> BlockAndUnlockAccount(Guid id)
        {
            try
            {
                var model = _context.ApplicationUsers.SingleOrDefault(x => x.Id == id.ToString());
                model.LockoutEnabled = (model.LockoutEnabled == (false));
                _context.Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ValidateUser(string id)
        {
            var model = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (model == null || model.EmailConfirmed) return false;
            model.EmailConfirmed = true;
            _context.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            var model = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Email.Equals(email));
            if (model != null) return model;
            return null;
        }

        private async Task<ApplicationUser> FindUserByCode(string code) => await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.ConcurrencyStamp == code);

        public async Task<bool> ChangePassword(string code, string newPassword)
        {
            var user = await FindUserByCode(code);
            if (user == null) return false;
            var newpass = _userManager.PasswordHasher.HashPassword(user, newPassword);
            user.PasswordHash = newpass;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<string> GetEmailTemplateToCreateAccount(string userId)
        {
            var html = string.Empty;
            var template = await _context.EmailTemplates.FirstOrDefaultAsync(x => x.Type == DataLayer.Enums.Base.TemplateTypeEnum.AccountConfirmed);
            html = template.Body.Replace("{url}",$"{_internalOptions.BaseUrl}{_options.UrlConfirmEmail}{userId}");
            return html;
        }

        public async Task<string> GetEmailTemplateToRecoveryAccount(string currencyStamUser)
        {
            var html = string.Empty;
            var template = await _context.EmailTemplates.FirstOrDefaultAsync(x => x.Type == DataLayer.Enums.Base.TemplateTypeEnum.PasswordRecovery);
            html = template.Body.Replace("{url}", $"{_internalOptions.BaseUrl}{_options.UrlRecovery}{currencyStamUser}");
            return html;
        }
    }
}
