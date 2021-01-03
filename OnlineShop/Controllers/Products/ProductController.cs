using BussinesLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using OnlineShop.ExtensionMethods;
using Service.Commons;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICommon _common;
        private readonly IUnitOfWork _services;
        public ProductController(IUnitOfWork services,
            ICommon common)
        {
            _services = services;
            _common = common;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Index() => View();
        [Authorize(Roles = "admin")]

        [HttpGet]
        public async Task<IActionResult> Create() => View(new ProductCategoryVM { Categories = await _services.CategoryService.GetList() });

        [Authorize(Roles = "admin")]
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
                Categories = await _services.CategoryService.GetList()
            };
            if (ModelState.IsValid)
            {
                if (await _services.ProductService.Add(product))
                {
                    return RedirectToAction("Products", "Admin");
                }
                return View(pvm);
            }
            return View(pvm);
        }
        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id) => Ok(await _services.ProductService.SoftRemove(id));
        
        [Authorize(Roles = "user")]
        public IActionResult Car() => View();
        [Authorize(Roles = "admin")]

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _services.ProductService.GetById(id);
            if (product != null)
            {
                var pvm = new ProductCategoryVM
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Brand = product.Brand,
                    Model = product.Model,
                    Quantity = product.Quantity,
                    CompanyName = product.CompanyName,
                    CategoryId = product.CategoryId,
                    Categories = await _services.CategoryService.GetList()
                };
                return View(pvm);
            }
            return RedirectToAction("Products", "Admin");
        }
        [Authorize(Roles = "admin")]

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
                if (await _services.ProductService.Update(product))
                {
                    TempData["Product"] = "Producto Actualizado";
                    return RedirectToAction("Products", "Admin");
                }
                pvm.Categories = await _services.CategoryService.GetList();
                return View(pvm);
            }
            return View(pvm);
        }
        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task<IActionResult> UploadPic(PicVM<Guid> model)
        {
            var file = await _common.UploadPic(model.Img);
            if (!string.IsNullOrEmpty(file))
            {
                if (ModelState.IsValid)
                {
                    if (await _services.ProductService.UplodadPic(new ProductPic
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
        [Authorize(Roles = "admin")]

        [HttpGet]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var model = await _services.ProductService.GetById(id, x => x.Category, x => x.ProductPics);
            if (model != null) return View(model);
            return new NotFoundView();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _services.ProductService.GetById(id, x => x.Category, x => x.ProductPics);
            if (model != null) return View(model);
            return new NotFoundView();
        }

    }
}