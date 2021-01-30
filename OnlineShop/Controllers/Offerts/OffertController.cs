using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using OnlineShop.Controllers.Base;
using OnlineShop.ExtensionMethods;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class OffertController : BaseController
    {
        private readonly IUnitOfWork _services;

        public OffertController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Offert model)
        {
            if (ModelState.IsValid)
            {
                if (await _services.OffertService.Add(model))
                {
                    SendNotification(null, "Agregado Correctamente");
                    return RedirectToAction("Offerts", "Admin");
                }
                SendNotification(null, "No se pudo agregar", NotificationEnum.Error);
                return View(model);
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) => Ok(await _services.OffertService.SoftRemove(id));

        [HttpGet]
        public async Task<IActionResult> Update(int id) => View(await _services.OffertService.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Update(Offert model)
        {
            if (ModelState.IsValid)
            {
                await _services.OffertService.Update(model);
                SendNotification(null, "Actualizada Correctamente");
                return RedirectToAction("Offerts", "Admin");
            }
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> UploadPic(ImageOffert model)
        {
            string file = await _services.ImageServerService.UploadImage(model.Img);
            SendNotification(null, "Intente de nuevo mas tarde", NotificationEnum.Error);
            if (!string.IsNullOrEmpty(file))
            {
                if (ModelState.IsValid)
                {
                    model.ImagePath = file;
                    var result = await _services.OffertService.UploadImg(model);
                    if (result)
                    {
                        SendNotification(null, "Imagen cargada correctamente");
                    }
                    else
                    {
                        SendNotification(null, "Intente de nuevo mas tarde", NotificationEnum.Error);
                    }
                }
            }
            return RedirectToAction("Offerts", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _services.OffertService.GetById(id, x => x.ImageOfferts);
            if (model != null) return View(model);
            return new NotFoundView();
        }

    }
}