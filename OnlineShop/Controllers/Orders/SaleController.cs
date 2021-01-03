using BussinesLayer.UnitOfWork;
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

        [Authorize(Roles = "user")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove(Guid id) => Ok(await _services.SaleService.SoftRemove(id));

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