using Model.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public abstract class BaseModel<T> where T : IEquatable<T>
    {
        public virtual T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public State State { get; set; }
        [NotMapped]
        public string CreatedAtSrt => CreatedAt.ToString("dd/MM/yyyy");
        [NotMapped]
        public string UpdatedAtStr => CreatedAt.ToString("dd/MM/yyyy");

    }
}
