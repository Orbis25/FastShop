using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels
{
    public class ProductPicVM
    {
        [Required]
        public IFormFile Img { get; set; }
        [Required]
        public Guid Id { get; set; }
    }
}
