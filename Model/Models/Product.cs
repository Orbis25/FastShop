using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Product : CommonProperties<Guid>
    {
        [Required]
        [Display(Name = "Nombre del Producto")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Compañia")]
        public string CompanyName { get; set; }
        [Display(Name = "Marca")]
        [Required]
        public string Brand { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        public string Model { get; set; }
        public int Pounts { get; set; }
        [Required]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual List<ProductPic> ProductPics { get; set; }
    }
}
