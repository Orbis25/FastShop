using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.Models;
using Model.Settings;
using Model.ViewModels;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _services;
        private readonly EmailSetting _options;
        private readonly InternalConfiguration _internalOptions;

        public HomeController(UserManager<ApplicationUser> user,
             IUnitOfWork services,
             IOptions<EmailSetting> options,
             IOptions<InternalConfiguration> intenalOpt)
        {
            userManager = user;
            _services = services;
            _options = options.Value;
            _internalOptions = intenalOpt.Value;
        }

        public async Task<IActionResult> Index()
        {

            return View(new HomeVM
            {
                Offert = await _services.OffertService.GetActiveOffert(),
                Products = await _services.ProductService.GetHomeProducts()
            });
        }

        public IActionResult Contact() => View();

        [HttpGet]
        public async Task<IActionResult> RecoveryPassword(string email) 
        {
            var model = await _services.AccountService.GetByEmail(email);
            if (model == null) return BadRequest("Invalid email");
            var html = await _services.AccountService.GetEmailTemplateToRecoveryAccount(model.ConcurrencyStamp);
            return Ok(await _services.EmailService.Send(new() { Body = html,To = model.Email , Subject = "Recuperar Contraseña" }));

        }
        
        [HttpGet]
        public IActionResult Changepassword(string code) => View(nameof(Changepassword), code);

        [HttpGet]
        public async Task<IActionResult> Change(string code, string newpass) => Ok(await _services.AccountService.ChangePassword(code, newpass));
    }
}
