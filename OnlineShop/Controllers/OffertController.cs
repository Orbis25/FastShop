using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Service.Commons;
using Service.Interface;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class OffertController : Controller
    {
        private readonly IOffertService _offertService;
        private readonly ICommon _common;

        public OffertController(IOffertService offertService, ICommon common)
        {
            _offertService = offertService;
            _common = common;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Offert model)
        {
            if (ModelState.IsValid)
            {
                if (await _offertService.Add(model))
                {
                    TempData["Offert"] = "Agregado Correctamente";
                    return RedirectToAction("Offerts", "Admin");
                }
                TempData["Offert"] = "No se pudo agregar";
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) => Ok(await _offertService.Remove(id));

        [HttpGet]
        public async Task<IActionResult> Update(int id) => View(await _offertService.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Update(Offert model)
        {
            if (ModelState.IsValid)
            {
                await _offertService.Update(model);
                TempData["Offert"] = "Actualizada Correctamente";
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
                    if (await _offertService.UploadImg(new ImageOffert
                    {
                        ImageName = file,
                        OffertId = model.Id,
                    }))
                    {
                        TempData["Offert"] = "Imagen cargada correctamente";
                    }
                }
            }
            return RedirectToAction("Offerts", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _offertService.GetById(id);
            return View(model);
        }

    }
}