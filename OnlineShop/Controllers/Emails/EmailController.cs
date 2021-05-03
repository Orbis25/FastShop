using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Emails;
using DataLayer.Utils.Paginations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Emails
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class EmailController : BaseController
    {
        private readonly IUnitOfWork _services;
        public EmailController(IUnitOfWork services) => _services = services;

        [HttpGet]
        public IActionResult Create(string userId)
        {
            ViewBag.UserId = userId;
            return PartialView("_CreatePartial");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Email model)
        {
            if (!ModelState.IsValid)
            {
                SendNotification("Hay datos que son requeridos, intente de nuevo", GetModelStateErrorSummary(ModelState), NotificationEnum.Error);
            }

            var user = await _services.UserService.Get(model.UserId);
            model.To = user.Email;
            var result = await _services.EmailService.Send(model);
            if (!result)
            {
                SendNotification("Ha ocurrido un error, intente de nuevo mas tarde", null, NotificationEnum.Error);
            }
            else
            {
                SendNotification("Correo enviado!");
            }
            await _services.EmailService.Add(model);
            return RedirectToAction("Users", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Index(PaginationBase pagination)
            => View(await _services.EmailService.GetAllPaginated(pagination));
    }
}
