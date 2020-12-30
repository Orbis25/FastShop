using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Cupon : BaseModel<int>
    {
        [Required]
        public string Concept { get; set; }

        public string Code { get; set; }

        [Required]
        public double Amount { get; set; }
        [Required]
        public StateCuppon StateOfCuppon { get; set; }
    }
}
