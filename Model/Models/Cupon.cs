using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Cupon : CommonProperties<int>
    {
        [Required]
        public string Concept { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public double Amount { get; set; }
        [Required]
        public StateCuppon State { get; set; }
    }
}
