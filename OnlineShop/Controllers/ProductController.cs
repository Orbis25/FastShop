using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Service.Commons;
using Service.Interface;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;
        private readonly ICommon _common;
        public ProductController(IProductService service,
            ICategoryService categoryService,
            ICommon common)
        {
            _service = service;
            _categoryService = categoryService;
            _common = common;
        }

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> Create() => View(new ProductCategoryVM { Categories = await _categoryService.GetAll()});

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            product.CreatedAt = DateTime.Now;
            var pvm = new ProductCategoryVM
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Brand = product.Brand,
                Model = product.Model,
                CompanyName = product.CompanyName,
                CategoryId = product.CategoryId,
                Categories = await _categoryService.GetAll()
            };
            if (ModelState.IsValid)
            {
                if (await _service.Add(product))
                {
                    return RedirectToAction("Products", "Admin");
                }
                return View(pvm);
            }
            return View(pvm);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id) => Ok(await _service.Remove(id));

        public IActionResult Car() => View();

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _service.GetById(id);
            if (product != null)
            {
                var pvm = new ProductCategoryVM
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Brand = product.Brand,
                    Model = product.Model,
                    CompanyName = product.CompanyName,
                    CategoryId = product.CategoryId,
                    Categories = await _categoryService.GetAll()
                };
                return View(pvm);
            }
            return RedirectToAction("Products", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            product.UpdatedAt = DateTime.Now;
            var pvm = new ProductCategoryVM
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Brand = product.Brand,
                Model = product.Model,
                CompanyName = product.CompanyName,
                CategoryId = product.CategoryId
            };
            if (ModelState.IsValid)
            {
                if (await _service.Update(product))
                {
                    TempData["Product"] = "Producto Actualizado";
                    return RedirectToAction("Products", "Admin");
                }
                pvm.Categories = await _categoryService.GetAll();
                return View(pvm);
            }
            return View(pvm);
        }

        [HttpPost]
        public async Task<IActionResult> UploadPic(PicVM<Guid> model)
        {
            var file = await _common.UploadPic(model.Img);
            if (!string.IsNullOrEmpty(file)) { 
                if (ModelState.IsValid)
                {
                    if (await _service.UplodadPic(new ProductPic
                    {
                        ProductId = model.Id,
                        CreatedAt = DateTime.Now,
                        PicName = file
                    }))
                    {
                        TempData["Product"] = "Imagen cargada correctamente";
                    }
                }
            }
            return RedirectToAction("Products", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(Guid id) => View(await _service.GetById(id));

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id) => View(await _service.GetById(id));

    }
}