﻿using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Commons;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly IUnitOfWork _services;
        private readonly ICommon _common;
        public SaleController(IUnitOfWork services, ICommon common)
        {
            _services = services;
            _common = common;
        }

        [Authorize(Roles = nameof(AuthLevel.User))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Sale model)
        {
            if (User.Identity.IsAuthenticated)
            {
                model.ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(await _services.SaleService.CreateSale(model, User.Identity.Name));
            }
            return Ok(false);
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpGet]
        public async Task<IActionResult> SaleDetail(Guid id)
        {
            var model = await _services.SaleService.GetById(id, x => x.DetailSales);
            if (model != null)
            {
                ViewData["StatusPercent"] = _common.OrderStatusPercent(model.Order.StateOrder);
                return View(model);
            }
            return RedirectToAction("Index", "admin");

        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _services.SaleService.SoftRemove(id);
            if (!result) return BadRequest("ha ocurrido un error, intente de nuevo mas tarde");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order model, Guid saleId)
        {
            if (ModelState.IsValid)
            {
                await _services.OrderService.Update(model);
            }
            return RedirectToAction(nameof(SaleDetail), new { id = saleId });
        }

        [HttpGet]
        public IActionResult PaypalPayment() => PartialView();
    }
}