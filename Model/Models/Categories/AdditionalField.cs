using DataLayer.Enums.Categories;
using DataLayer.Models.Base;
using DataLayer.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Categories
{
    public class AdditionalField : BaseModel<int>
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Description { get; set; }
        [Display(Name = "Tipo")]
        public AditionalFieldEnum Type { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsRequired { get; set; }
        public bool Selectionable { get; set; }

        public IEnumerable<ProductDetail> ProductDetail { get; set; }

        [NotMapped]
        public string IsRequiredStr => IsRequired ? "Si" : "No";
        [NotMapped]
        public string SelectionableStr => Selectionable ? "Si" : "No";

    }
}
