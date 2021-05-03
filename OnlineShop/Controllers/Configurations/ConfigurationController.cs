using BussinesLayer.UnitOfWork;
using Commons.Helpers;
using DataLayer.Enums.Base;
using DataLayer.ViewModels.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class ConfigurationController : BaseController
    {
        private readonly IUnitOfWork _services;
        public ConfigurationController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _services.ConfigurationService.GetEmailConfiguration());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmail(ConfigurationEmailVM model)
        {
            if (!ModelState.IsValid) return View(nameof(Index), model);

            var entity = await _services.ConfigurationService.GetById(model.Id);
            if (entity == null)
            {
                var created = await _services.ConfigurationService.Add(new()
                {
                    EmailSender = model.EmailSender,
                    PasswordEmail = model.PasswordEmail.ToBase64String()
                });
                if (!created) SendNotification("Ha ocurrido un error", null, NotificationEnum.Error);
                else SendNotification("Actualizado correctamente");
            }
            else
            {
                entity.EmailSender = model.EmailSender;
                entity.PasswordEmail = model.PasswordEmail.ToBase64String();
                var updated = await _services.ConfigurationService.Update(entity);
                if (!updated) SendNotification("Ha ocurrido un error", null, NotificationEnum.Error);
                else SendNotification("Actualizado correctamente");
            }
            return RedirectToAction(nameof(Index), entity);
        }
    }
}
