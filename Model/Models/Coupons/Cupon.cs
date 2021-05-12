﻿using Commons.Helpers;
using DataLayer.Models.Base;
using Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    public class Cupon : BaseModel<int>
    {
        [Required(ErrorMessage = "El {0} es requerido.")]
        [MaxLength(150, ErrorMessage = "El {0} es demaciado largo.")]
        [Display(Name = "Concepto")]
        public string Concept { get; set; }
        [MaxLength(6)]
        public string Code { get; set; } = StringHelper.GetRandomCode();
        [Required(ErrorMessage = "El {0} es requerido.")]
        [Display(Name = "Monto")]
        public double Amount { get; set; }
        [Display(Name = "Estado del cupón")]
        [Required]
        public StateCuppon StateOfCuppon { get; set; }
        public bool IsByPercent { get; set; } 

        [NotMapped]
        public bool IsValid => StateOfCuppon == StateCuppon.Valid;

        public string FormatedAmount => IsByPercent ? $"{Amount}%" : Amount.ToString("C");

    }
}
