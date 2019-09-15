using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interface;

namespace OnlineShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _service;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public AdminController(ICategoryService categoryService ,
            IAdminService adminService ,
            IUserService userService,
            IProductService productService)
        {
            _categoryService = categoryService;
            _service = adminService;
            _userService = userService;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> Users() => View(await _userService.GetUsers());

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            return View(await _productService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Categories() => View(await _categoryService.GetAll());

        [HttpGet]
        public IActionResult Offerts() => View();

        [HttpGet]
        public IActionResult Cupons() => View();

        [HttpGet]
        public IActionResult Sales() => View();
    }
}