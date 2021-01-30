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
            UserManager<ApplicationUser> userManager
            )
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

        public async Task<bool> SendEmailConfirmation(string email, string userId)
        {
            var html = $"Porfavor pulsa el siguiente boton para confirmar tu cuenta <br /> <br><br> " +
                $"<a style='text-decoration:none;border:none;border-radius:5px;font-size:15px;background:#1976d2;padding:10px;color:#fff;cursor:pointer;' " +
                $"href={_internalOptions.BaseUrl}{_options.UrlConfirmEmail}{userId}>CONFIRMAR</a> <br /><br><br> @copyright  {_internalOptions.AppName}";

            using var smtp = new SmtpClient(_options.Smtp,_options.Port)
            {
                Host = _options.Smtp,
                EnableSsl = true,
                UseDefaultCredentials = _options.DefaultCredentials,
                Credentials = new NetworkCredential(_options.User, _options.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_options.User)
                };
                mailMessage.To.Add(email);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = $"{_internalOptions.AppName} Account";
                mailMessage.Body = html;
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception)
            { return false; }
            return true;
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

        public async Task<bool> SendEmailRecoveryPass(string email)
        {
            var model = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == email);
            var html = $"Porfavor pulsa el siguiente boton para cambiar tu contraseña <br /> <a style='border:none;border-radius:5px;font-size:15px;background:red;padding:10px;color:#fff;cursor:pointer;' " +
                $" href={_internalOptions.BaseUrl}{_options.UrlRecovery}{model.ConcurrencyStamp}>PULSAME<a/> " +
                $"<br /> @copyright  {_internalOptions.AppName}";

            var smtp = new SmtpClient()
            {
                Host = _options.Smtp,
                EnableSsl = true,
                UseDefaultCredentials = _options.DefaultCredentials,
                Credentials = new NetworkCredential(_options.User, _options.Password)
            };

            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_options.User)
                };
                mailMessage.To.Add(email);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = $"{_internalOptions.AppName} Account";
                mailMessage.Body = html;
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception)
            { return false; }
            return true;
        }

    }
}
