using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Service.Commons;
using Service.Interface;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class TrackingController : Controller
    {
        private readonly IOrderService _service;
        private readonly ICommon _common;
        private readonly IAccountService _account;


        public TrackingController(IOrderService service , ICommon common, IAccountService account)
        {
            _service = service;
            _common = common;
            _account = account;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string OrderCode)
        {
            var user = await _account.GetByEmail(User.Identity.Name);
            var result = await _service.FindByOrderCode(OrderCode , user.Id);
            if (result == null)
            {
                ViewData["NotFound"] = true;
                return View();
            }
            ViewData["StatusPercent"] = _common.OrderStatusPercent(result.StateOrder);
            return View(result);
        }

       
    }
}