using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Users() => View();

        [HttpGet]
        public IActionResult Products() => View();

        [HttpGet]
        public IActionResult Categories() => View();

        [HttpGet]
        public IActionResult Offerts() => View();

        [HttpGet]
        public IActionResult Cupons() => View();

        [HttpGet]
        public IActionResult Sales() => View();
    }
}