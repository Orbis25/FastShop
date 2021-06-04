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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ProductDetail model)
        {
            if (!ModelState.IsValid) return BadRequest("Algunos valores son incorrectos");
            var result = await _services.ProductDetailService.Add(model);
            if (!result) return BadRequest("Ha ocurrido un error intente de nuevo mas tarde");
            return Ok(result);
        }
           
        public IActionResult Index()
        {
            return View();
        }
    }
}
