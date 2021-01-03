using BussinesLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using System;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _services;
        public CategoryController(IUnitOfWork services)
        {
              _services = services;
        }
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Index(int take = 9 , int index = 1) => View(new ShopVM { 
            Categories = await _services.CategoryService.GetList(),
            Products = await _services.ProductService.GetAllPaginateProducts(take,index)
        });

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                model.CreatedAt = DateTime.Now;
                var result = await _services.CategoryService.Add(model);
                if (result != false) return Ok(result);
            }
           return BadRequest();
        }
        
        [Authorize(Roles = "admin")]
        [HttpGet]
       public async Task<IActionResult> Remove([FromRoute] int id)
       {
            if (await _services.CategoryService.SoftRemove(id)) return Ok(true);
            return BadRequest("not deleted");
       }
        [Authorize(Roles = "admin")]
        [HttpPost]
       public async Task<IActionResult> Update([FromBody] Category model)
       {
            if (await _services.CategoryService.Update(model)) return Ok(true);
            return BadRequest("not updated");
       }
       [Authorize(Roles = "user,admin")]
       [HttpGet]
       public async Task<IActionResult> Filter(Filter filter)
       {
            
          return View(nameof(Index), new ShopVM
          {  
              Categories = await _services.CategoryService.GetList(),
              Products = await _services.ProductService.Filter(filter),
              Filters = filter
          });
       }

    }
}