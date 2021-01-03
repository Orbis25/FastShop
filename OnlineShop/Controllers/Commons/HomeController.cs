using BussinesLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Service.Commons;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICommon _commonService;
        private readonly IUnitOfWork _services;

        public HomeController(UserManager<ApplicationUser> user,
             ICommon common,
             IUnitOfWork services)
        {
            _commonService = common;
            userManager = user;
            _services = services;
        }

        #region To AddUser to Role 
        [Authorize(Roles = "admin")]
        public async Task AddUserToRole()
        {
            //var roles = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //await roles.CreateAsync(new IdentityRole("admin"));
            //await roles.CreateAsync(new IdentityRole("user"));

            var resul = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin",
                PhoneNumber = "admin",
                Email = "admin@admin.com"
            }, "admin123");
            if (resul.Succeeded)
            {
                var model = await userManager.FindByEmailAsync("admin");
                await userManager.AddToRoleAsync(model, "admin");
            }

        }
        #endregion

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
        public async Task<IActionResult> RecoveryPassword(string email) => Ok(await _commonService.SendEmailRecoveryPass(email));

        [HttpGet]
        public IActionResult Changepassword(string code) => View(nameof(Changepassword), code);

        [HttpGet]
        public async Task<IActionResult> Change(string code, string newpass) => Ok(await _commonService.ChangePassWord(code, newpass));
    }
}
