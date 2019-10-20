using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Service.Interface;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly ISaleService _service;
        public SaleController(ISaleService sale) => _service = sale;
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]Sale model)
        {
            if (User.Identity.IsAuthenticated)
            {
                model.ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Ok(await _service.CreateSale(model,User.Identity.Name));
            }
            return Ok(false);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> SaleDetail(Guid id) => View(await _service.GetById(id));

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Remove(Guid id) => Ok(await _service.Remove(id));
    }
}