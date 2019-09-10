using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IServiceProvider _serviceProvider;
        public HomeController(UserManager<ApplicationUser> user ,
            SignInManager<ApplicationUser> app , IServiceProvider serviceProvider)
        {
            userManager = user;
            signInManager = app;
            _serviceProvider = serviceProvider;
        }

        #region To AddUser to Role 
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

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Contact() => View();

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
