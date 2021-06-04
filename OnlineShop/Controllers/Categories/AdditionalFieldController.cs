using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Categories
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class AdditionalFieldController : BaseController
    {
        private readonly IUnitOfWork _services;
        public AdditionalFieldController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult AddField(int categoryId)
        {
            ViewBag.CategoryId = categoryId;
            return PartialView("_AddFieldPartial");
        }

        [HttpPost]
        public async Task<IActionResult> AddField(AdditionalField model)
        {
            ViewBag.CategoryId = model.CategoryId;
            var exist = await _services.AdditionalFieldService.Exist(x => x.Name == model.Name && x.CategoryId == model.CategoryId);
            if (exist)
            {
                SendNotification("Este campo ya existe", null, NotificationEnum.Warning);
                return RedirectToAction("Edit", "Category", new { Id = model.CategoryId });
            }
            if (!ModelState.IsValid)
            {
                SendNotification("Ha ocurrido un error", "Intente de nuevo mas tarde", NotificationEnum.Error);
            }
            var result = await _services.AdditionalFieldService.Add(model);
            if (!result)
            {
                SendNotification("Ha ocurrido un error", "Intente de nuevo mas tarde", NotificationEnum.Error);
            }
            else
            {
                SendNotification("Agregado correctamente");
            }
            return RedirectToAction("Edit", "Category", new { Id = model.CategoryId });
        }


        [HttpGet]
        public async Task<IActionResult> RemoveField(int categoryId, int id)
        {
            var result = await _services.AdditionalFieldService.Remove(id);
            if (!result) SendNotification("ha ocurrido un error", null, NotificationEnum.Error);
            SendNotification("Eliminado correctamente.");
            return RedirectToAction("Edit", "Category", new { Id = categoryId });
        }


        [HttpGet]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            return Ok(await _services.AdditionalFieldService.GetList(x => x.CategoryId == categoryId, x => x.ProductDetail));
        }

    }
}
