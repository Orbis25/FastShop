using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Models.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDetail model)
        {
            var result = await _services.ProductDetailService.Add(model);
            if (!result) return BadRequest("Intente de nuevo mas tarde");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var result = await _services.ProductDetailService.Remove(id);
            if (!result) return BadRequest("Intente de nuevo mas tarde");
            return Ok(result);
        }

        /// <summary>
        /// Get the products details
        /// </summary>
        /// <param name="id">productId</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id)
        {
            return PartialView("_GetAllPartial", await _services.ProductDetailService.GetList(x => x.ProductId == id));
        }
    }
}
