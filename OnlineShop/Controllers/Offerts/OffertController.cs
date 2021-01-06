using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using OnlineShop.Controllers.Base;
using OnlineShop.ExtensionMethods;
using Service.Commons;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class OffertController : BaseController
    {
        private readonly IUnitOfWork _services;
        private readonly ICommon _common;

        public OffertController(IUnitOfWork services, ICommon common)
        {
            _services = services;
            _common = common;
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
            else
            {
                return View(model);
            }
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

        public async Task<IActionResult> UploadPic(PicVM<int> model)
        {
            string file = await _common.UploadPic(model.Img);
            if (!string.IsNullOrEmpty(file))
            {
                if (ModelState.IsValid)
                {
                    if (await _services.OffertService.UploadImg(new ImageOffert
                    {
                        ImageName = file,
                        OffertId = model.Id,
                    }))
                    {
                        SendNotification(null, "Imagen cargada correctamente");
                    }
                }
            }
            return RedirectToAction("Offerts", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _services.OffertService.GetById(id);
            if (model != null) return View(model);
            return new NotFoundView();
        }

    }
}