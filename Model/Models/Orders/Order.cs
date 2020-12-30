using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Order : CommonProperties<Guid>
    {
        public StateOrder StateOrder { get; set; } = StateOrder.Storage;
        public string Description { get; set; }
        public Sale Sale { get; set; }
        public string OrderCode { get; set; }
    }
}
