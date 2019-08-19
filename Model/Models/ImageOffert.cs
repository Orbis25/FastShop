using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models
{
    public class ImageOffert
    {
        public int Id { get; set; }
        [ForeignKey("OffertId")]
        public int OffertId { get; set; }
        public Offert Offert { get; set; }
        [Required]
        public string ImageName { get; set; }
    }
}
