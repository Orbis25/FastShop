using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Service.Interface;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        private readonly IProductService _productService;
        public CategoryController(ICategoryService service , IProductService productService)
        {
              _service = service;
            _productService = productService;
        }
        public async Task<IActionResult> Index(int take = 9 , int page = 1) => View(new ShopVM { 
            Categories = await _service.GetAll(),
            Products = await _productService.GetAllPaginateProducts(take,page)
        });
        

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                model.CreatedAt = DateTime.Now;
                var result = await _service.Add(model);
                if (result != false) return Ok(result);
            }
           return BadRequest();
        }

       [HttpGet]
       public async Task<IActionResult> Remove([FromRoute] int id)
       {
            if (await _service.Remove(id)) return Ok(true);
            return BadRequest("not deleted");
       }

       [HttpPost]
       public async Task<IActionResult> Update([FromBody] Category model)
       {
            if (await _service.Update(model)) return Ok(true);
            return BadRequest("not updated");
       }

        [HttpGet]
        public async Task<IActionResult> Filter(Filter filter)
        {
            
            return View(nameof(Index), new ShopVM
            {
                Categories = await _service.GetAll(),
                Products = await _productService.Filter(filter),
                Filters = filter
            });
        }

    }
}