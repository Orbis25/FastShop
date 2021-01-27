using BussinesLayer.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Commons;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class TrackingController : Controller
    {
        private readonly ICommon _common;
        private readonly IUnitOfWork _services;

        public TrackingController(IUnitOfWork services, ICommon common)
        {
            _services = services;
            _common = common;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string OrderCode)
        {
            var user = await _services.AccountService.GetByEmail(User.Identity.Name);
            var result = await _services.OrderService.FindByOrderCode(OrderCode, user.Id);
            if (result == null)
            {
                ViewData["NotFound"] = true;
                return View();
            }
            ViewData["StatusPercent"] = _services.OrderService.OrderStatusPercent(result.StateOrder);
            return View(result);
        }


    }
}