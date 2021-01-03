using BussinesLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class CupponController : Controller
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
                    TempData["Cuppon"] = "Agregado Correctamente";
                    return RedirectToAction("Cupons", "Admin");
                }
                TempData["Cuppon"] = "Ha ocurrido un error al agregar";
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
                    TempData["Cuppons"] = "Actualizado Correctamente";
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