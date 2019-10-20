using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Commons;
using Service.Interface;

namespace OnlineShop.Controllers
{
    public class CupponController : Controller
    {
        private readonly ICupponService _service;
        private readonly ICommon _common;

        public CupponController(ICupponService service , ICommon common)
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
        public async Task<IActionResult> Remove(int id) => Ok(await _service.Remove(id));
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetByCupponCode(string code) => Ok(await _service.GetByCupponCode(code));
    }
}