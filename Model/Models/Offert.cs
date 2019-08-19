using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Offert : CommonProperties<int>
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Duration { get; set; }
        [Required]
        public string Concept { get; set; }
        public virtual List<ImageOffert> ImageOfferts { get; set; }
    }
}
