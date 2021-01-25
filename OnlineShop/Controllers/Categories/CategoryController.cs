using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly IUnitOfWork _services;
        public CategoryController(IUnitOfWork services)
        {
            _services = services;
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpGet]
        public IActionResult Create() => View();

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid) return View(model);
            var exist = await _services.CategoryService.GetList(x => x.Name == model.Name);
            if (exist.Any())
            {
                SendNotification("Error", "Ya existe una categoria con ese nombre", NotificationEnum.Error);
                return View(model);
            }
            var result = await _services.CategoryService.Add(model);
            if (result == false)
            {
                SendNotification("Ocurrio un error", "intente de nuevo mas tarde.", NotificationEnum.Error);
                return View(model);
            }
            SendNotification("Creada Correctamente!");
            return RedirectToAction("Categories", "Admin");
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var result = await _services.CategoryService.SoftRemove(id);
            if (!result) return BadRequest("not deleted");
            return Ok(result);
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Category model)
        {
            if (!ModelState.IsValid) return BadRequest("El Nombre es requerido");
            var exist = await _services.CategoryService.GetList(x => x.Name == model.Name);
            if (exist.Any()) return BadRequest("Ya existe una categoria con ese nombre");
            var result = await _services.CategoryService.Update(model);
            if (result == false) return BadRequest("Ha ocurrido un error, intente de nuevo mas tarde");
            return Ok(result);
        }
    }
}