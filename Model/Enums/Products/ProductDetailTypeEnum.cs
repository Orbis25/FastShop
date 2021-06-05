using System.ComponentModel.DataAnnotations;

namespace DataLayer.Enums.Products
{
    public enum ProductDetailTypeEnum
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
