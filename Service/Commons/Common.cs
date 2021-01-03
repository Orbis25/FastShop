using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Enums;
using Model.Models;
using Model.Settings;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commons
{
    public interface ICommon
    {
        Task<string> UploadPic(IFormFile file);
        Task<bool> SendEmailRecoveryPass(string email);
        Task<bool> ChangePassWord(string code, string newPassword);
        int OrderStatusPercent(StateOrder state);

    }

    public class Common : ICommon
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSetting _settings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InternalConfiguration _internalOptions;

        public Common(ApplicationDbContext context , IOptions<EmailSetting> options , 
            UserManager<ApplicationUser> userManager,
            IOptions<InternalConfiguration> internalConfigurations)
        {
            _context = context;
            _settings = options.Value;
            _userManager = userManager;
            _internalOptions = internalConfigurations.Value;
        }
        private static bool CheckImg(string file)
        {
            if (file.Contains(".png") || file.Contains(".jpeg") || file.Contains(".jpg") ) return true;
            return false;
        }

        public async Task<string> UploadPic(IFormFile file)
        {
            if (CheckImg(file.FileName))
            {
                var fileName = Path.GetFileName(file.FileName);
                var format = fileName[^4..];
                fileName = fileName.Replace(fileName, $"{Guid.NewGuid()}{format}");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", fileName);
                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                   
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }
            return null;
        }

        public async Task<bool> SendEmailRecoveryPass(string email)
        {
            var model = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Email == email);
            var html = $"Porfavor pulsa el siguiente boton para cambiar tu contraseña <br /> <a style='border:none;border-radius:5px;font-size:15px;background:red;padding:10px;color:#fff;cursor:pointer;' " +
                $" href={_internalOptions.BaseUrl}{_settings.UrlRecovery}{model.ConcurrencyStamp}>PULSAME<a/> " +
                $"<br /> @copyright  {_internalOptions.AppName}";
           
            var smtp = new SmtpClient()
            {
                Host = _settings.Smtp,
                EnableSsl = true,
                UseDefaultCredentials = _settings.DefaultCredentials,
                Credentials = new NetworkCredential(_settings.User, _settings.Password)
            };
            
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.User)
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

        public async Task<bool> ChangePassWord(string code, string newPassword)
        {
            var user = await FindUserByCode(code);
            if (user == null) return false;
            var newpass = _userManager.PasswordHasher.HashPassword(user, newPassword);
            user.PasswordHash = newpass;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public int OrderStatusPercent(StateOrder state)
        {
            return state switch
            {
                StateOrder.Storage => 25,
                StateOrder.Send => 50,
                StateOrder.Delivered => 100,
                _ => 100,
            };
        }
    }
}
