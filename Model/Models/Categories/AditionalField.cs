using DataLayer.Enums.Categories;
using DataLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Categories
{
    public class AditionalField : BaseModel<int>
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Description { get; set; }
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public AditionalFieldEnum Type { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsRequired { get; set; }
    }
}
