using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Products
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class ProductDetailController : Controller
    {
        private readonly IUnitOfWork _services;
        public ProductDetailController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Create() => PartialView("_CreatePartial");
        

    }
}
