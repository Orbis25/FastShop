using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Offert : BaseModel<int>
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Duration { get; set; }
        [Required]
        public string Concept { get; set; }
        public ICollection<ImageOffert> ImageOfferts { get; set; }
    }
}
