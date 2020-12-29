using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class PicVM<T> where T : struct
    {
        [Required]
        public IFormFile Img { get; set; }
        [Required]
        public T Id { get; set; }
    }
}
