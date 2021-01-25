using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using OnlineShop.Controllers.Base;
using OnlineShop.ExtensionMethods;
using Service.Commons;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class ProductController : BaseController
    {

        /// <summary>
        /// TODO: ME QUEDE AQUI PARA OPTIMIZAR EL CODIGO DE LOS PRODUCTOS.
        /// </summary>
        private readonly ICommon _common;
        private readonly IUnitOfWork _services;
        public ProductController(IUnitOfWork services,
            ICommon common)
        {
            _services = services;
            _common = common;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int take = 9, int index = 1) => View(new ShopVM
        {
            Categories = await _services.CategoryService.GetList(),
            Products = await _services.ProductService.GetAllPaginateProducts(take, index)
        });

        [HttpGet]
        public async Task<IActionResult> Filter(Filter filter)
        {

            return View(nameof(Index), new ShopVM
            {
                Categories = await _services.CategoryService.GetList(),
                Products = await _services.ProductService.Filter(filter),
                Filters = filter
            });
        }


        [Authorize(Roles = nameof(AuthLevel.Admin))]

        [HttpGet]
        public async Task<IActionResult> Create() => View(new ProductCategoryVM { Categories = await _services.CategoryService.GetList() });

        [Authorize(Roles = nameof(AuthLevel.Admin))]
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
        [Authorize(Roles = nameof(AuthLevel.Admin))]

        [HttpPost]
        public async Task<IActionResult> Remove(Guid id) {
            var result = await _services.ProductService.SoftRemove(id);
            if(!result) return BadRequest("Ocurrio un error, intente de nuevo mas tarde");
            return Ok(result);
        }

        [Authorize(Roles = nameof(AuthLevel.User))]
        public IActionResult Car() => View();
        [Authorize(Roles = nameof(AuthLevel.Admin))]

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
        [Authorize(Roles = nameof(AuthLevel.Admin))]

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
                    SendNotification(null, "Producto Actualizado");
                    return RedirectToAction("Products", "Admin");
                }
                pvm.Categories = await _services.CategoryService.GetList();
                return View(pvm);
            }
            return View(pvm);
        }
        [Authorize(Roles = nameof(AuthLevel.Admin))]

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
                        SendNotification(null, "Imagen cargada correctamente");

                    }
                }
            }
            return RedirectToAction("Products", "Admin");
        }
        [Authorize(Roles = nameof(AuthLevel.Admin))]

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