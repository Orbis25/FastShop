using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Sale : CommonProperties<Guid>
    {
        [Required]
        public Guid UserId { get; set; }
        public string CuponCode { get; set; }
        [Required]
        public double Total { get; set; }
        public List<DetailSale> DetailSales { get; set; }
    }

    public class DetailSale : CommonProperties<Guid>
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
