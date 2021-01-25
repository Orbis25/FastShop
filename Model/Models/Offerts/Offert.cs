using DataLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class Offert : BaseModel<int>
    {
        [Required(ErrorMessage = "La {0} es requerida.")]
        [MaxLength(100,ErrorMessage = "Escriba una {0} mas corta.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Required(ErrorMessage = "La {0} es requerida.")]
        [Display(Name = "Duración")]
        public DateTime Duration { get; set; }
        [MaxLength(20, ErrorMessage = "El {0} es demaciado largo.")]
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Concepto")]
        public string Concept { get; set; }
        [Display(Name = "Imagenes")]
        public ICollection<ImageOffert> ImageOfferts { get; set; }
    }
}
