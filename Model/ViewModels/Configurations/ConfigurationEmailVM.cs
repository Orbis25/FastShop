using Commons.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels.Configurations
{
    public class ConfigurationEmailVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Correo")]
        public string EmailSender { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "La {0} es requerido")]
        public string PasswordEmail { get; set; }

        public string PasswordEmailPlain => string.IsNullOrEmpty(PasswordEmail) ? null : PasswordEmail.GetFromBase64();
    }
}
