using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Service.Commons;
using Service.Interface;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly ISaleService _service;
        private readonly IOrderService _order;
        private readonly ICommon _common;
        public SaleController(ISaleService sale , IOrderService order , ICommon common)
        {
            _service = sale;
            _order = order;
            _common = common;
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Sale model)
        {
            if (User.Identity.IsAuthenticated)
            {
                model.ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(await _service.CreateSale(model,User.Identity.Name));
            }
            return Ok(false);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SaleDetail(Guid id)
        {
            var model = await _service.GetById(id);
            if (model != null)
            {
                ViewData["StatusPercent"] = _common.OrderStatusPercent(model.Order.StateOrder);
                return View(model);
            }
            return RedirectToAction("Index", "admin");

        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove(Guid id) => Ok(await _service.Remove(id));

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order model, Guid saleId)
        {
            if (ModelState.IsValid)
            {
               await _order.Update(model);
            }
            return RedirectToAction(nameof(SaleDetail), new { id = saleId });
        }

        [HttpGet]
        public IActionResult PaypalPayment() => PartialView();
    }
}