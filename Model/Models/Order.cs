using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        [Required]
        public StateOrder StateOrder { get; set; } = StateOrder.Storage;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public virtual Sale Sale { get; set; }
        public string OrderCode { get; set; }
        public State State { get; set; } = State.active;
    }
}
