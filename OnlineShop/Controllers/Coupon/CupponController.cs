using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.ViewModels.Coupon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using OnlineShop.Controllers.Base;
using OnlineShop.ExtensionMethods;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class CupponController : BaseController
    {
        private readonly IUnitOfWork _services;

        public CupponController(IUnitOfWork services)
        {
            _services = services;
        }
        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpGet]
        public IActionResult Add() => View();
        [Authorize(Roles = nameof(AuthLevel.Admin))]

        [HttpPost]
        public async Task<IActionResult> Add(Cupon model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsByPercent && model.Amount > 100)
                {
                    ModelState.AddModelError(nameof(Cupon.Amount), "No puede colocar mayor al 100%");
                }
                else
                {
                    if (await _services.CouponService.Add(model))
                    {
                        SendNotification(null, "Agregado Correctamente");
                        return RedirectToAction("Cupons", "Admin");
                    }
                }
                SendNotification(null, "Ha ocurrido un error al agregar", NotificationEnum.Error);
            }
            return View(model);
        }
        [Authorize(Roles = nameof(AuthLevel.Admin))]

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _services.CouponService.GetById(id);
            if (model == null) return new NotFoundView();
            return View(model);
        }
        [Authorize(Roles = nameof(AuthLevel.Admin))]

        [HttpPost]
        public async Task<IActionResult> Update(Cupon model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsByPercent && model.Amount > 100)
                {
                    ModelState.AddModelError(nameof(Cupon.Amount), "No puede colocar mayor al 100%");
                }
                else
                {
                    if (await _services.CouponService.Update(model))
                    {
                        SendNotification(null, "Actualizado Correctamente");
                        return RedirectToAction("Cupons", "Admin");
                    }
                }
                SendNotification(null, "Ha ocurrido un error al actualizar", NotificationEnum.Error);
            }
            return View(model);
        }
        [Authorize(Roles = nameof(AuthLevel.Admin))]

        [HttpPost]
        public async Task<IActionResult> Remove(int id) => Ok(await _services.CouponService.SoftRemove(id));
        [Authorize(Roles = nameof(AuthLevel.User))]
        [HttpGet]
        public async Task<IActionResult> GetByCupponCode(string code) => Ok(await _services.CouponService.GetByCupponCode(code));

        [HttpPost]
        public async Task<IActionResult> UpdateState([FromBody] CouponUpdateVM model)
        {
            if (model == null) return BadRequest("Error");
            var result = await _services.CouponService.SetValidOrInvalid(model);
            if (!result) return BadRequest("Error, intenta de nuevo");
            return Ok(result);
        }
    }
}