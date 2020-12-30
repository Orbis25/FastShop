using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using Model.ViewModels;
using OnlineShop.Models;
using Service.Commons;
using Service.Interface;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        //private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IOffertService _offertService;
        private readonly IProductService _productService;
        private readonly ICommon _commonService;
        public HomeController(UserManager<ApplicationUser> user,
            //SignInManager<ApplicationUser> app,
            IOffertService offert,
             IProductService productService,
             ICommon common)
        {
            userManager = user;
           // signInManager = app;
            _offertService = offert;
            _productService = productService;
            _commonService = common;
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
                Offert = await _offertService.GetActiveOffert(),
                Products = await _productService.GetHomeProducts()
            });
        }

        public IActionResult Contact() => View();

        [HttpGet]
        public async Task<IActionResult> RecoveryPassword(string email) => Ok(await _commonService.SendEmailRecoveryPass(email));

        [HttpGet]
        public IActionResult Changepassword(string code) =>  View(nameof(Changepassword),code);

        [HttpGet]
        public async Task<IActionResult> Change(string code, string newpass) => Ok(await _commonService.ChangePassWord(code, newpass));
    }
}
