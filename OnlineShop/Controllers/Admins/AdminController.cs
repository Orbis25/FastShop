using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Accounts;
using DataLayer.ViewModels.Coupon;
using DataLayer.ViewModels.Orders;
using DataLayer.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Base;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = nameof(AuthLevel.Admin))]
    public class AdminController : BaseController
    {
        private readonly IUnitOfWork _services;

        public AdminController(IUnitOfWork services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> Users(UserFilterVM pagination) => View(await _services.UserService.GetAllPaginated(pagination));

        [HttpGet]
        public async Task<IActionResult> Products(ProductFilterVM filters) => View(await _services.ProductService.GetAllPaginateProducts(filters));

        [HttpGet]
        public async Task<IActionResult> Categories(PaginationBase pagination, string name)
        {
            ViewBag.Name = name;
            var result = await _services.CategoryService.GetAllPaginated(pagination,
                         (string.IsNullOrEmpty(name) ? null : x => x.Name.Contains(name)));
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Offerts(PaginationBase pagination, string q)
        {
            ViewBag.Q = q;
            return View(await _services.OffertService.filter(pagination, q));
        }

        [HttpGet]
        public async Task<IActionResult> Cupons(PaginationBase pagination, CouponFilterVM filters)
        {
            ViewBag.Filter = filters;
            return View(await _services.CouponService.GetFiltered(pagination, filters));
        }

        [HttpGet]
        public async Task<IActionResult> Sales(SaleFilterVM filters)
        {
            return View(await _services.SaleService.GetSales(filters));
        }

        [HttpGet]
        public async Task<IActionResult> MetricsDashboard()
        {
            ViewBag.Clients = await _services.AccountService.Count();
            ViewBag.Products = _services.ProductService.GetAll().Count().ToString("N0");
            ViewBag.SalesAmount = _services.SaleService.GetAll().Sum(x => x.Total).ToString("C");
            ViewBag.Reviews = _services.ReviewService.GetAll().Count().ToString("N0");
            return PartialView("_MetricsDashboardPartial");
        }

    }
}