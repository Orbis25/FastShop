using Commons.Helpers;
using DataLayer.Models.Base;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class Sale : BaseModel<Guid>
    {
        [Display(Name = "Codigo del cupón")]
        public string CuponCode { get; set; }
        [Required]
        public double Total { get; set; }
        public double SubTotal { get; set; }
        [Required]
        [Display(Name = "Dirreción")]
        public string Address { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [Required]
        public string ApplicationUserId { get; set; }
        [Display(Name = "Codigo")]
        public string Code { get; set; } = StringHelper.GetRandomCode();
        public ApplicationUser User { get; set; }
        [Display(Name = "Detalle")]
        public List<DetailSale> DetailSales { get; set; }
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
        [Display(Name = "Tipo de pago")]
        [Required]
        public PaymentType PaymentType { get; set; } = PaymentType.Cash;

        public string Country { get; set; }
        public string City { get; set; }

        [NotMapped]
        public string WithCoupon => CuponCode ?? "N/A";


    }

    public class DetailSale : BaseModel<Guid>
    {
        [Display(Name = "Cantidad")]
        [Required]
        public int Quantity { get; set; }
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
