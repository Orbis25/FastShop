using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _services;
        public AccountController(IUnitOfWork services) => _services = services;

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        public async Task<IActionResult> BlockOrUnlockAccount(Guid id)
        {
            await _services.AccountService.BlockAndUnlockAccount(id);
            return RedirectToAction("Users", "Admin");
        }

        [Authorize(Roles =  nameof(AuthLevel.Admin) + "," + nameof(AuthLevel.User))]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var model = await _services.AccountService.GetByEmail(User.Identity.Name);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult EmailConfirm() => View();

        [HttpGet]
        public async Task<IActionResult> Validate(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                if (await _services.AccountService.ValidateUser(code))
                {
                    ViewData["validate"] = true;
                }
            }
            return View();
        }
    }
}