using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using OnlineShop.Controllers.Base;
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
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Add() => View();
        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task<IActionResult> Add(Cupon model)
        {
            if (ModelState.IsValid)
            {
                if (await _services.CouponService.Add(model))
                {
                    SendNotification(null, "Agregado Correctamente");
                    return RedirectToAction("Cupons", "Admin");
                }
                SendNotification(null, "Ha ocurrido un error al agregar", NotificationEnum.Error);
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _services.CouponService.GetById(id);
            if (model != null)
            {
                return View(model);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task<IActionResult> Update(Cupon model)
        {
            if (ModelState.IsValid)
            {
                if (await _services.CouponService.Update(model))
                {
                    SendNotification(null, "Actualizado Correctamente");
                    return RedirectToAction("Cupons", "Admin");
                }
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task<IActionResult> Remove(int id) => Ok(await _services.CouponService.SoftRemove(id));
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetByCupponCode(string code) => Ok(await _services.CouponService.GetByCupponCode(code));
    }
}