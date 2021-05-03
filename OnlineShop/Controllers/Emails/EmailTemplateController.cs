using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Emails.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Base;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Emails
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class EmailTemplateController : BaseController
    {
        private readonly IUnitOfWork _services;
        public EmailTemplateController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => PartialView("_GetAllPartial", await _services.EmailTemplateService.GetList());

        [HttpPost]
        public async Task<IActionResult> Update(EmailTemplate model)
        {
            if (!ModelState.IsValid)
            {
                SendNotification("Intente nuevamente", GetModelStateErrorSummary(ModelState), NotificationEnum.Warning);
                return RedirectToAction("Index", "Configuration");
            }

            if (!_services.EmailTemplateService.HaveRequireParameters(model.Body, model.Type))
            {
                var pr = _services.EmailTemplateService.GetParameters(model.Type);
                SendNotification("Error", $"Por favor revise que algunos de estos parametros : {pr} se encuentren en el cuerpo de la plantilla"
                    , NotificationEnum.Error);
                return RedirectToAction("Index", "Configuration");
            }

            if (await _services.EmailTemplateService.ExistTemplate(model.Type))
            {
                SendNotification("Ya existe este tipo de plantilla", null, NotificationEnum.Warning);
                return RedirectToAction("Index", "Configuration");
            }

            var result = await _services.EmailTemplateService.Update(model);
            if (!result) SendNotification("Ha ocurrido un problema. Intente de nuevo mas tarde", null, NotificationEnum.Error);
            else SendNotification("Plantilla Actualizada");
            return RedirectToAction("Index", "Configuration");
        }

        [HttpGet]
        public IActionResult Add() => PartialView("_AddEmailTemplatePartial");

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
            => PartialView("_GetByIdPartial", await _services.EmailTemplateService.GetById(id));

        [HttpGet]
        public IActionResult GetParameters(TemplateTypeEnum type)
            => Ok(_services.EmailTemplateService.GetParameters(type));
    }
}
