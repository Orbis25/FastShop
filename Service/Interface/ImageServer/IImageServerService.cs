using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.ImageServer
{
    public interface IImageServerService
    {

        /// <summary>
        /// Upload image to ingur api
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Task<string> UploadImage(IFormFile image);
        Task<string> UploadImage(IFormFile file, string path, string folder, string lastFile = null);
    }
}
