using DataLayer.Models.Base;
using DataLayer.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Product : BaseModel<Guid>
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre del Producto")]
        [MaxLength(50,ErrorMessage = "El {0} es demaciado largo.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Precio")]
        public double Price { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Compañia")]
        [MaxLength(30, ErrorMessage = "La {0} es demaciada larga.")]
        public string CompanyName { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(30,ErrorMessage = "La {0} es demaciada larga")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Modelo")]
        [MaxLength(30, ErrorMessage = "El {0} es demaciado largo.")]
        public string Model { get; set; }
        [Display(Name = "Puntos")]
        public int Pounts { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductPic> ProductPics { get; set; }
    }
}
