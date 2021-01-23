using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Category : BaseModel<int>
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(40,ErrorMessage = "El nombre de la {0} es muy grande.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name = "Productos")]
        public ICollection<Product> Products { get; set; }
    }
}
