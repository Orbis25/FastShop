using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Service.Interface;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> Create() => View(new ProductCategoryVM { Categories = await _categoryService.GetAll() });

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (await _service.Add(product))
                {
                    return RedirectToAction("Products", "Admin");
                }
                return View(product);
            }
            return View(product);
        }


        public IActionResult Car() => View();


    }
}