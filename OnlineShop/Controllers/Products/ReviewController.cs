using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Products;
using DataLayer.Utils.Paginations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Products
{
    [Authorize(Roles = nameof(AuthLevel.User))]
    public class ReviewController : BaseController
    {
        private readonly IUnitOfWork _services;
        public ReviewController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Create(Guid productId)
        {
            ViewBag.ProductId = productId;
            return PartialView("_CreatePartial");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _services.ReviewService.GetById(id);
            if (result == null) return BadRequest("La reseña no fue encontrada");
            return PartialView("_CreatePartial",result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Review model)
        {
            var exist = await _services.ReviewService.ExistReview(model.ProductId, GetLoggedIdUser());
            if (exist)
            {
                SendNotification("Ya ha compartido su reseña", "Lo sentimos pero solo puede brindar su reseña 1 sola vez", NotificationEnum.Info);
                return RedirectToAction(nameof(ProductController.GetById), "Product", new { Id = model.ProductId });

            }
            if (!ModelState.IsValid)
            {
                SendNotification("Algunos valores son requeridos", GetModelStateErrorSummary(ModelState), NotificationEnum.Warning);
            }
            else
            {
                model.UserId = GetLoggedIdUser();
                var result = await _services.ReviewService.Add(model);
                if (!result)
                {
                    SendNotification("Ha ocurrido un error, intente de nuevo mas tarde", null, NotificationEnum.Error);
                }
                else
                {
                    SendNotification("Reseña agregada, Gracias!");
                }
            }

            return RedirectToAction(nameof(ProductController.GetById), "Product", new { Id = model.ProductId });
        }


        [HttpPost]
        public async Task<IActionResult> Update(Review model)
        {
            var exist = await _services.ReviewService.ExistReview(model.ProductId, GetLoggedIdUser());
            if (exist)
            {
                SendNotification("Ya ha compartido su reseña", "Lo sentimos pero solo puede brindar su reseña 1 sola vez", NotificationEnum.Info);
                return RedirectToAction(nameof(ProductController.GetById), "Product", new { Id = model.ProductId });

            }
            if (!ModelState.IsValid)
            {
                SendNotification("Algunos valores son requeridos", GetModelStateErrorSummary(ModelState), NotificationEnum.Warning);
            }
            else
            {
                model.UserId = GetLoggedIdUser();
                var result = await _services.ReviewService.Add(model);
                if (!result)
                {
                    SendNotification("Ha ocurrido un error, intente de nuevo mas tarde", null, NotificationEnum.Error);
                }
                else
                {
                    SendNotification("Reseña Actualizada, Gracias!");
                }
            }

            return RedirectToAction(nameof(ProductController.GetById), "Product", new { Id = model.ProductId });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(PaginationBase paginationBase, Guid productId)
        {
            var result = await _services.ReviewService.GetAllPaginated(paginationBase, x => x.ProductId == productId, null, x => x.User);
            return PartialView("_GetAllPartial", result);
        }

 

    }
}
