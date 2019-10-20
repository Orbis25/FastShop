using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Interface;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _service;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOffertService _offertService;
        private readonly ICupponService _cupponService;
        private readonly ISaleService _saleService;

        public AdminController(ICategoryService categoryService ,
            IAdminService adminService ,
            IUserService userService,
            IProductService productService ,
            ICupponService cuppon,
            IOffertService offertService,
            ISaleService saleService)
        {
            _categoryService = categoryService;
            _service = adminService;
            _userService = userService;
            _productService = productService;
            _offertService = offertService;
            _cupponService = cuppon;
            _saleService = saleService;
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
        public async Task<IActionResult> Offerts() => View(await _offertService.GetAll());

        [HttpGet]
        public async Task<IActionResult> Cupons() => View(await _cupponService.GetAll());

        [HttpGet]
        public async Task<IActionResult> Sales() => View(await _saleService.GetAll());
    }
}