using Model.Enums;
using System;

namespace Model.Models
{
    public class CommonProperties<T> where T : struct
    {
        public virtual T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public State State { get; set; }

    }
}
