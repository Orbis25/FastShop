using Commons.Helpers;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Order : BaseModel<Guid>
    {
        public StateOrder StateOrder { get; set; } = StateOrder.Storage;
        public string Description { get; set; }
        public Sale Sale { get; set; }
        public string OrderCode { get; set; } = StringHelper.GetRandomCode(8);
    }
}
