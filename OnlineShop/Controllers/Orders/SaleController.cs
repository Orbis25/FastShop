using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using OnlineShop.Controllers.Base;
using OnlineShop.ExtensionMethods;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public async Task<IActionResult> Add(Sale model)
        {
            var cartItems = await _services.CartItemService.GetList(x => x.UserId == GetLoggedIdUser(),x => x.Product);
            if (!cartItems.Any())
            {
                SendNotification("no hay items", null, NotificationEnum.Error);
                return RedirectToAction("Index", "CartItem");
            }
            var user = await _services.UserService.Get(GetLoggedIdUser());
            if(string.IsNullOrEmpty(user.Address))
            {
                SendNotification("este usuario necesita tener una dirrección valida", null, NotificationEnum.Error);
                return RedirectToAction("Index", "CartItem");
            }
            model.Description = user.Address;
            model.ApplicationUserId = GetLoggedIdUser();
            var result = await _services.SaleService.CreateSale(cartItems.ToList(),model, User.Identity.Name);
            if (!result) 
            {
                SendNotification("Ha ocurrido un error, intente de nuevo mas tarde",null,NotificationEnum.Error);
                return RedirectToAction("Index", "CartItem");
            }
            await _services.CartItemService.ClearCart(GetLoggedIdUser());
            SendNotification("Compra exitosa!");
            return RedirectToAction(nameof(ShoppingDetail), new { id = model.Id });
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

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order model, Guid saleId)
        {
            if (ModelState.IsValid)
            {
                await _services.OrderService.Update(model);
            }
            return RedirectToAction(nameof(SaleDetail), new { id = saleId });
        }

        [HttpGet("shoppingDetail/{id}")]
        public async Task<IActionResult> ShoppingDetail([Required]Guid id)
        {
            var model = await _services.SaleService.GetById(id, x => x.DetailSales, x=> x.User);
            if (model == null) return new NotFoundView();
            ViewData["StatusPercent"] = _services.OrderService.OrderStatusPercent(model.Order.StateOrder);
            return View(model);
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpGet]
        public async Task<IActionResult> SendOrderEmail(Guid id)
        {
            var order = await _services.SaleService.GetById(id, x=> x.User,x => x.Order);
            if (order == null) return BadRequest("Orden no encontrada");
            var result = await _services.SaleService.SendOrderEmail(order, order.User.Email);
            if (!result) return BadRequest("Ha ocurrido un error, intente de nuevo mas tarde");
            return Ok("Orden Enviada");
        }
    
    }
}