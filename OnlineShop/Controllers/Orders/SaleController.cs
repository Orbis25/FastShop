using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using OnlineShop.Controllers.Base;
using OnlineShop.ExtensionMethods;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class SaleController : BaseController
    {
        private readonly IUnitOfWork _services;
        public SaleController(IUnitOfWork services)
        {
            _services = services;
        }

        [Authorize(Roles = nameof(AuthLevel.User))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Sale model)
        {
            model.ApplicationUserId = GetLoggedIdUser();
            var result = await _services.SaleService.CreateSale(model, User.Identity.Name);
            if (!result) return BadRequest("Ha ocurrido un error, intente de nuevo mas tarde");
            return Ok(result);
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpGet]
        public async Task<IActionResult> SaleDetail(Guid id)
        {
            var model = await _services.SaleService.GetById(id, x => x.DetailSales);
            if (model == null) return new NotFoundView();
            ViewData["StatusPercent"] = _services.OrderService.OrderStatusPercent(model.Order.StateOrder);
            return View(model);
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