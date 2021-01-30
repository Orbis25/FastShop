using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models;
using Model.ViewModels;
using OnlineShop.Controllers.Base;
using OnlineShop.ExtensionMethods;
using Service.Commons;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class ProductController : BaseController
    {

        private readonly ICommon _common;
        private readonly IUnitOfWork _services;
        public ProductController(IUnitOfWork services,
            ICommon common)
        {
            _services = services;
            _common = common;
        }

        [HttpGet]
        public async Task<IActionResult> Index(ProductFilterVM filters)
        {
            var categories = await _services.CategoryService.GetList();
            ViewBag.Categories = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(await _services.ProductService.GetAllPaginateProducts(filters));
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _services.CategoryService.GetList();
            ViewBag.Categories = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View();
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {

            var categories = await _services.CategoryService.GetList();
            ViewBag.Categories = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            if (!ModelState.IsValid) return View(product);

            var result = await _services.ProductService.Add(product);
            if (!result)
            {
                SendNotification("Error", "Ha ocurrido un error, intente de nuevo mas tarde", NotificationEnum.Error);
                return View(product);
            }
            SendNotification("Producto agregado");
            return RedirectToAction("Products", "Admin");

        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _services.ProductService.SoftRemove(id);
            if (!result) return BadRequest("Ocurrio un error, intente de nuevo mas tarde");
            return Ok(result);
        }

        [Authorize(Roles = nameof(AuthLevel.User))]
        public IActionResult Car() => View();


        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _services.ProductService.GetById(id);
            if (product == null) return new NotFoundView();
            var categories = await _services.CategoryService.GetList();
            ViewBag.Categories = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(product);
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            var exist = await _services.ProductService.Exist(product.Id);
            if (exist) return new NotFoundView();

            var categories = await _services.CategoryService.GetList();
            ViewBag.Categories = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

            if (!ModelState.IsValid) return View(product);

            var result = await _services.ProductService.Update(product);
            if (!result)
            {
                SendNotification("Error", "Ha ocurrido un error, intente de nuevo mas tarde", NotificationEnum.Error);
                return View(product);
            }

            SendNotification("Producto actualizado");
            return RedirectToAction("Products", "Admin");

        }
        [Authorize(Roles = nameof(AuthLevel.Admin))]


        [HttpPost]
        public async Task<IActionResult> UploadPic(ProductPic model)
        {
            var file = await _services.ImageServerService.UploadImage(model.Img);
            SendNotification(null, "Intente de nuevo mas tarde", NotificationEnum.Error);
            if (!string.IsNullOrEmpty(file))
            {
                if (ModelState.IsValid)
                {
                    model.Path = file;
                    var result = await _services.ProductService.UplodadPic(model);
                    if (!result)
                    {
                        SendNotification(null, "Ha ocurrido un problema, intente de nuevo mas tarde", NotificationEnum.Error);
                    }
                    else
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