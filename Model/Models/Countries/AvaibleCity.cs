using DataLayer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Countries
{
    public class AvaibleCity : BaseModel<int>
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Name { get; set; }

        [Display(Name = "Codigo de pais")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string CountryCode { get; set; }

        [Display(Name = "Latitud")]
        public string Lat { get; set; }
        [Display(Name = "Longitud")]
        public string Long { get; set; }
    }
}
