using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Enums
{
    public enum StateOrder
    {
        [Display(Name = "ALMACEN")]
        Storage,
        [Display(Name = "ENVIADA")]
        Send,
        [Display(Name = "ENTREGADA")]
        Delivered,
        [Display(Name = "CANCELADA")]
        Canceled
    }
}
