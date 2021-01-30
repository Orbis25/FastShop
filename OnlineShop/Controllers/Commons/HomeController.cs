using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _services;

        public HomeController(UserManager<ApplicationUser> user,
             IUnitOfWork services)
        {
            userManager = user;
            _services = services;
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
        public async Task<IActionResult> RecoveryPassword(string email) => Ok(await _services.AccountService.SendEmailRecoveryPass(email));

        [HttpGet]
        public IActionResult Changepassword(string code) => View(nameof(Changepassword), code);

        [HttpGet]
        public async Task<IActionResult> Change(string code, string newpass) => Ok(await _services.AccountService.ChangePassword(code, newpass));
    }
}
