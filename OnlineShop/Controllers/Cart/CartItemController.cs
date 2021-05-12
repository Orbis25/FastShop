using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Cart;
using DataLayer.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Controllers.Base;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Cart
{
    [Authorize(Roles = nameof(AuthLevel.User))]
    public class CartItemController : BaseController
    {
        private readonly IUnitOfWork _services;
        public CartItemController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var coupon = await _services.CouponService.GetByCupponCode(code);
                if (coupon != null)
                {
                    ViewBag.coupon = coupon;
                }
            }
            var result = await _services.CartItemService.GetList(x => x.UserId == GetLoggedIdUser(), x => x.Product);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CartItem model)
        {

            var exist = _services.CartItemService.GetAll(x => x.ProductId == model.ProductId);
            if (await exist.AnyAsync())
            {
                SendNotification("Ya tienes este producto en tu carrito", null, NotificationEnum.Warning);
                return RedirectToAction("GetById", "Product", new { id = model.ProductId });
            }

            if (model.Quantity <= 0)
            {
                SendNotification("Cantidad invalida", null, NotificationEnum.Error);
                return RedirectToAction("GetById", "Product", new { id = model.ProductId });
            }
            model.UserId = GetLoggedIdUser();
            var result = await _services.CartItemService.Add(model);
            if (!result)
            {
                SendNotification("ha ocurrido un error al agregar al carrito intente de nuevo mas tarde", null, NotificationEnum.Error);
                return RedirectToAction("GetById", "Product", new { id = model.ProductId });
            }
            SendNotification("Producto agregado al carrito");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _services.CartItemService.Remove(id);
            if (!result) return BadRequest("Ha ocurrido un error intente de nuevo mas tarde");
            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetTotal() => Ok(await _services.CartItemService.GetTotal(GetLoggedIdUser()));

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] CartItemUpdateVM model)
        {
            var result = await _services.CartItemService.UpdateItem(model);
            if (!result) return BadRequest(result);
            return Ok(result);
        }
    }
}
