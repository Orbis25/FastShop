using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Car() => View();
    }
}