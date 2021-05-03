using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Enums.Base
{
    public enum TemplateTypeEnum
    {
        [Display(Name = "Recuperar contraseñas")]
        PasswordRecovery,
        [Display(Name = "Usuario bloqueado")]
        LockedUser,
        [Display(Name = "Confirmar cuenta")]
        AccountConfirmed,
        [Display(Name = "Factura")]
        Bill
    }
}
