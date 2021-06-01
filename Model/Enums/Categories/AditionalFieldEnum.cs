using System.ComponentModel.DataAnnotations;

namespace DataLayer.Enums.Categories
{
    public enum AditionalFieldEnum
    {
        [Display(Name = "Texto")]
        Text,
        [Display(Name = "Numerico")]
        Numeric,
        [Display(Name = "Boleano (Si o No)")]
        Boolean,
        [Display(Name = "Fecha")]
        DateStr
    }
}
