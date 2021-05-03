using BussinesLayer.Interface.Emails;
using BussinesLayer.Repository;
using DataLayer.Models.Emails;
using DataLayer.ViewModels.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Settings;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Emails
{
    public class EmailService : BaseRepository<Email,ApplicationDbContext,Guid> , IEmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSetting _options;
        public EmailService(ApplicationDbContext context,IOptions<EmailSetting> options ): base(context)
        {
            _context = context;
            _options = options.Value;
        }

        public async Task<bool> Send(Email model)
        {
            var credentials = await _context.Configurations.Select(x => new ConfigurationEmailVM { EmailSender = x.EmailSender, PasswordEmail = x.PasswordEmail })
                .FirstOrDefaultAsync();

            using var smtp = new SmtpClient(_options.Smtp, _options.Port)
            {
                Host = _options.Smtp,
                EnableSsl = true,
                UseDefaultCredentials = _options.DefaultCredentials,
                Credentials = new NetworkCredential(credentials.EmailSender, credentials.PasswordEmailPlain),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(credentials.EmailSender)
                };
                mailMessage.To.Add(model.To);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = model.Subject;
                mailMessage.Body = model.Body;
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception)
            { return false; }
            return true;
        }
    }
}
