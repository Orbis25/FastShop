using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
