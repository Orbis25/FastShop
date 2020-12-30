using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Commons;
using Service.Interface;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class CupponController : Controller
    {
        private readonly ICouponService _service;
        private readonly ICommon _common;

        public CupponController(ICouponService service , ICommon common)
        {
            _service = service;
            _common = common;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Add() => View();
        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task<IActionResult> Add(Cupon model)
        {
            model.Code = _common.GenerateCodeString(5);
            if (ModelState.IsValid)
            {
                if(await _service.Add(model))
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
            var model = await _service.GetById(id);
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
                if(await _service.Update(model))
                {
                    TempData["Cuppons"] = "Actualizado Correctamente";
                    return RedirectToAction("Cupons", "Admin");
                }
            }
            return View(model);
        }
        [Authorize(Roles = "admin")]

        [HttpPost]
        public async Task<IActionResult> Remove(int id) => Ok(await _service.SoftRemove(id));
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetByCupponCode(string code) => Ok(await _service.GetByCupponCode(code));
    }
}