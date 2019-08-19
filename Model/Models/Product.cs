using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Product : CommonProperties<Guid>
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public int Pounts { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual List<ProductPic> ProductPics { get; set; }
    }
}
