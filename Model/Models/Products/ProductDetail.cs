using DataLayer.Models.Base;
using DataLayer.Models.Categories;
using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Products
{
    public class ProductDetail : BaseModel<int>
    {
        public int AdditionalFieldId { get; set; }
        public AditionalField AditionalField { get; set; }
        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Campo {0} es requerido")]
        public string Value { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
