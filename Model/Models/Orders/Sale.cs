using Commons.Helpers;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class Sale : BaseModel<Guid>
    {

        public string CuponCode { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        [Required]
        public string ApplicationUserId { get; set; }
        public string Code { get; set; } = StringHelper.GetRandomCode();
        public ApplicationUser User { get; set; }
        public List<DetailSale> DetailSales { get; set; }
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; } = PaymentType.Cash;
        
    }

    public class DetailSale : BaseModel<Guid>
    {
        [Required]
        public int Quantity { get; set; }
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
