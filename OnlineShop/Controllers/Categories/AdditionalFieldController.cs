using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Categories
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class AdditionalFieldController : Controller
    {
        private readonly IUnitOfWork _services;
        public AdditionalFieldController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
