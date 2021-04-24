using BussinesLayer.Interface.ImageServer;
using Commons.Helpers;
using DataLayer.Settings.ImageServer;
using DataLayer.ViewModels.Base.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BussinesLayer.Services.ImageServer
{
    public class ImageServerService : IImageServerService
    {
        private readonly ImageServerOption _options;
        public ImageServerService(IOptions<ImageServerOption> options)
        {
            _options = options.Value;
        }
        public async Task<string> UploadImage(IFormFile image)
        {
            using var httpClient = new HttpClient() { BaseAddress = new Uri(_options.BaseUrl) };
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Client-ID {_options.ClientId}");
            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpContent content = new StringContent("image");
            form.Add(content, "image");
            var stream = image.OpenReadStream();
            content = new StreamContent(stream);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "image",
                FileName = image.FileName
            };
            form.Add(content);
            var response = await httpClient.PostAsync(_options.UploadUrl, form);
            //deserialize object to get result
            var result = JsonConvert.DeserializeObject<ImageServerResponseVM>(await response.Content.ReadAsStringAsync());
            //save the image link in db
            return result.Data.Link;
        }


        private bool IsValidFormat(IFormFile file)
        {
            foreach (var extension in _options.Formats) if (Path.GetExtension(file.FileName).ToLower() == extension) return true;
            return false;
        }

        public void RemoveFile(string webRootPath, string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                File.Delete($@"{webRootPath}{_options.BasePath}\\{path}");
            }
        }

        public async Task<string> UploadImage(IFormFile file, string path, string folder, string lastFile = null)
        {
            string _path = $@"{path}{_options.BasePath}\{folder}";
            if (!IsValidFormat(file)) return string.Empty;
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);

            RemoveFile(path, lastFile);

            try
            {
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                string fileName = $"{Guid.NewGuid()}-{StringHelper.GetRandomCode()}{fileExtension}";
                using var stream = File.Create($@"{_path}//{fileName}");
                await file.CopyToAsync(stream);
                return $@"{folder}\{fileName}";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message ?? e.InnerException.Message);
            }
        }
    }
}
