using DataLayer.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class ProductPic : BaseModel<int>
    {
        public string PicName { get; set; }
        public string Path { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [NotMapped]
        [Required]
        public IFormFile Img { get; set; }
    }
}
