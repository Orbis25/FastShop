using DataLayer.Enums.Products;
using DataLayer.Models.Base;
using DataLayer.Models.Cart;
using DataLayer.Models.Categories;
using DataLayer.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class Product : BaseModel<Guid>
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre del Producto")]
        [MaxLength(50, ErrorMessage = "El {0} es demaciado largo.")]
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
        [MaxLength(30, ErrorMessage = "La {0} es demaciada larga")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Modelo")]
        [MaxLength(30, ErrorMessage = "El {0} es demaciado largo.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(100, ErrorMessage = "Ingrese un minimo de 100 caracteres")]
        public string Description { get; set; }
        public ICollection<ProductPic> ProductPics { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Review> Reviews { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }


        /// <summary>
        /// NotMapped properties
        /// </summary>
        [NotMapped]
        public ProductStatusEnum Status
             => Quantity <= (int)ProductStatusEnum.Spent ? ProductStatusEnum.Spent : 
                (Quantity == (int)ProductStatusEnum.AlmostSpent ? ProductStatusEnum.AlmostSpent : 
                ProductStatusEnum.Good);

    }
}
