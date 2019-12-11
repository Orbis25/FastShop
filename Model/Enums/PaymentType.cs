using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Enums
{
    public enum PaymentType
    {
        [Display(Name = nameof(Paypal))]
        Paypal,
        [Display(Name = nameof(Cash))]
        Cash
    }
}
