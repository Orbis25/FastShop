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
       
    }

    public class Common : ICommon
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSetting _settings;
        private readonly InternalConfiguration _internalOptions;

        public Common(ApplicationDbContext context, IOptions<EmailSetting> options,
            IOptions<InternalConfiguration> internalConfigurations)
        {
            _context = context;
            _settings = options.Value;
            _internalOptions = internalConfigurations.Value;
        }
        private static bool CheckImg(string file)
        {
            if (file.Contains(".png") || file.Contains(".jpeg") || file.Contains(".jpg")) return true;
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


    }
}
