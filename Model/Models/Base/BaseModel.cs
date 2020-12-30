using Model.Enums;
using System;

namespace Model.Models
{
    public class BaseModel<T> where T : IEquatable<T>
    {
        public virtual T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public State State { get; set; }

    }
}
