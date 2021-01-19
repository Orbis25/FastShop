using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _services;

        public AdminController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index() => View(_services.AdminService.GetAllCountServices());

        [HttpGet]
        public async Task<IActionResult> Users() => View(await _services.UserService.GetUsers());

        [HttpGet]
        public async Task<IActionResult> Products() => View(await _services.ProductService.GetList(null, x => x.Category));

        [HttpGet]
        public async Task<IActionResult> Categories() => View(await _services.CategoryService.GetList());

        [HttpGet]
        public async Task<IActionResult> Offerts() => View(await _services.OffertService.GetList(null, x => x.ImageOfferts));

        [HttpGet]
        public async Task<IActionResult> Cupons() => View(await _services.CouponService.GetList());

        [HttpGet]
        public async Task<IActionResult> Sales() => View(await _services.SaleService.GetList());
    }
}