using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Service.Commons
{
    public interface ICommon
    {
        Task<string> UploadPic(IFormFile file);
    }
    public class Commons : ICommon
    {
        private bool CheckImg(string file)
        {
            if (file.Contains(".png") || file.Contains(".jpeg") || file.Contains(".jpg") ) return true;
            return false;
        }

        public async Task<string> UploadPic(IFormFile file)
        {
            if (CheckImg(file.FileName))
            {
                var fileName = Path.GetFileName(file.FileName);
                var format = fileName.Substring(fileName.Length - 4);
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
