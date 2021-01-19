using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
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
        [Authorize(Roles = nameof(AuthLevel.User) + "," + nameof(AuthLevel.Admin))]
        public async Task<IActionResult> Index(int take = 9, int index = 1) => View(new ShopVM
        {
            Categories = await _services.CategoryService.GetList(),
            Products = await _services.ProductService.GetAllPaginateProducts(take, index)
        });

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                var result = await _services.CategoryService.Add(model);
                if (result != false) return Ok(result);
            }
            return BadRequest();
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var result = await _services.CategoryService.SoftRemove(id);
            if (!result) return BadRequest("not deleted");
            return Ok(result);
        }

        [Authorize(Roles = nameof(AuthLevel.Admin))]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Category model)
        {
            if (await _services.CategoryService.Update(model)) return Ok(true);
            return BadRequest("not updated");
        }

        [Authorize(Roles = nameof(AuthLevel.User) + "," + nameof(AuthLevel.Admin))]
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