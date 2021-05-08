using DataLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Countries
{
    public class AvaibleCountry : BaseModel<int>
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Name { get; set; }
        [Display(Name = "Codigo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(2)]
        [MinLength(2)]
        public string Iso3 { get; set; }
    }
}
