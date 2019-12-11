using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Models;
using Model.Settings;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSetting _options;
        private readonly InternalConfiguration _internalOptions;
        public AccountService(ApplicationDbContext context, IOptions<EmailSetting> options, IOptions<InternalConfiguration> internalOptions)
        {
            _context = context;
            _options = options.Value;
            _internalOptions = internalOptions.Value;
        }

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

        public async Task<bool> SendEmailConfirmation(string email)
        {
            var model = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email.Equals(email));
            if (model == null) return false;


            var html = $"Porfavor pulsa el siguiente boton para confirmar tu cuenta <br /> <br><br> " +
                $"<a style='text-decoration:none;border:none;border-radius:5px;font-size:15px;background:#1976d2;padding:10px;color:#fff;cursor:pointer;' " +
                $"href={_internalOptions.BaseUrl}{_options.UrlConfirmEmail}{model.Id}>CONFIRMAR</a> <br /><br><br> @copyright  {_internalOptions.AppName}";

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
