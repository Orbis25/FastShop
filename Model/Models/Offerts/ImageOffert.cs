using DataLayer.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models
{
    public class ImageOffert : BaseModel<int>
    {
        [ForeignKey("OffertId")]
        public int OffertId { get; set; }
        public Offert Offert { get; set; }
        [MaxLength(60)]
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
