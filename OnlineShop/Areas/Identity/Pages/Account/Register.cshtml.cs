using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Models;
using Model.Settings;
using Service.Interface;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IAccountService _service;
        private readonly EmailSetting _options;
        private readonly InternalConfiguration _internalOptions;
        private readonly IUnitOfWork _services;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            ILogger<RegisterModel> logger,
            IUnitOfWork service,
            IOptions<EmailSetting> options,
            IOptions<InternalConfiguration> internalConfiguration
            )
        {
            _userManager = userManager;
            _logger = logger;
            _service = service.AccountService;
            _options = options.Value;
            _internalOptions = internalConfiguration.Value;
            _services = service;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El campo {0} es requerido")]
            [EmailAddress]
            [Display(Name = "Correo")]
            public string Email { get; set; }


            [Required(ErrorMessage = "La campo {0} es requerida")]
            [Display(Name = "Dirreccion")]
            public string Address { get; set; }

            [Required(ErrorMessage = "El campo {0} es requerido")]
            [Display(Name = "Numero")]
            [Phone]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "El campo {0} es requerido")]
            [Display(Name = "Nombre completo")]
            public string FullName { get; set; }


            [Required(ErrorMessage = "El campo {0} es requerido")]
            [StringLength(100, ErrorMessage = "El {0} es invalido, ingrese una {0} mayor a 5 caracteres", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Repetir contraseña")]
            [Required(ErrorMessage = "El campo {0} es requerido")]
            [Compare("Password", ErrorMessage = "El campo contraseña y el campo {0} no son identicos")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Pais")]
            [Required(ErrorMessage = "El campo {0} es requerido")]
            public string Country { get; set; }
            [Display(Name = "Ciudad")]
            [Required(ErrorMessage = "El campo {0} es requerido")]
            public string City { get; set; }

        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Address = Input.Address,
                    PhoneNumber = Input.PhoneNumber,
                    FullName = Input.FullName,
                    Country = Input.Country,
                    City = Input.City
                };
                var template = await _services.AccountService.GetEmailTemplateToCreateAccount(user.Id);
                var emailSended = await _services.EmailService.Send(new() { Body = template, Subject = $"{_internalOptions.AppName} Account", To = user.Email });

                if (!emailSended)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrio un error, intente de nuevo mas tarde");
                    return Page();
                }

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, nameof(AuthLevel.User));
                    if (emailSended) return LocalRedirect("/Account/EmailConfirm");
                    else ModelState.AddModelError(string.Empty, "Occurrio un error, intente de nuevo");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
