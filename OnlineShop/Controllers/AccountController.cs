using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _account;
        public AccountController(IAccountService account) => _account = account;

        
        public async Task<IActionResult> BlockOrUnlockAccount(Guid id)
        {
           await _account.BlockAndUnlockAccount(id);   
            return RedirectToAction("Admin", "Users");
        }
    }
}