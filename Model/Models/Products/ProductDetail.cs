using DataLayer.Enums.Products;
using DataLayer.Models.Base;
using Model.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.Products
{
    public class ProductDetail : BaseModel<int>
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Campo {0} es requerido")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Campo {0} es requerido")]
        public string Description { get; set; }
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Campo {0} es requerido")]
        public string Value { get; set; }
        [Display(Name = "Tipo")]
        public ProductDetailTypeEnum Type { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [NotMapped]
        public string BooleanFormatedStr => Value == "true" ? "Si" : "No";
        [NotMapped]
        public string CheckboxValueFormat => Value == "true" ? "checked" : string.Empty;
    }
}
