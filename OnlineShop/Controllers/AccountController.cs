using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _account;
        public AccountController(IAccountService account) => _account = account;

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> BlockOrUnlockAccount(Guid id)
        {
           await _account.BlockAndUnlockAccount(id);   
            return RedirectToAction("Users","Admin");
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            var model = await _account.GetByEmail(User.Identity.Name);
            return Ok(model);
        }

        [HttpGet]
        public IActionResult EmailConfirm() => View();

        [HttpGet]
        public async Task<IActionResult> Validate(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                if(await _account.ValidateUser(code))
                {
                    ViewData["validate"] = true;
                }
            }
            return View();
        }
    }
}