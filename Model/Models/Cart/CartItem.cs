using DataLayer.Models.Base;
using Model.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Cart
{
    public class CartItem : BaseModel<int>
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Required(ErrorMessage = "La {0} es requerida")]
        [Display(Name = "Cantidad")]
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; }

    }
}
