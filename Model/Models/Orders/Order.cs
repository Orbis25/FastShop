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
        [Display(Name = "Estado de la orden")]
        public StateOrder StateOrder { get; set; } = StateOrder.Storage;
        [Display(Name = "Descripción")]
        [MaxLength(200,ErrorMessage = "La {0} es demaciada larga.")]
        public string Description { get; set; }
        public Sale Sale { get; set; }
        [Display(Name = "Codigo")]
        [MaxLength(8)]
        public string OrderCode { get; set; } = StringHelper.GetRandomCode(8);
    }
}
