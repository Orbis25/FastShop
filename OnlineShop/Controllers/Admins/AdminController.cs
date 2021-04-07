using BussinesLayer.UnitOfWork;
using DataLayer.Enums.Base;
using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Products;
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
        public async Task<IActionResult> Users(PaginationBase pagination) => View(await _services.UserService.GetAllPaginated(pagination));

        [HttpGet]
        public async Task<IActionResult> Products(ProductFilterVM filters) => View(await _services.ProductService.GetAllPaginateProducts(filters));
        
        [HttpGet]
        public async Task<IActionResult> Categories(PaginationBase pagination) => View(await _services.CategoryService.GetAllPaginated(pagination));

        [HttpGet]
        public async Task<IActionResult> Offerts(PaginationBase pagination) => View(await _services.OffertService.GetAllPaginated(pagination, null,null, x => x.ImageOfferts));

        [HttpGet]
        public async Task<IActionResult> Cupons(PaginationBase pagination) => View(await _services.CouponService.GetAllPaginated(pagination));

        [HttpGet]
        public async Task<IActionResult> Sales(PaginationBase pagination) => View(await _services.SaleService.GetAllPaginated(pagination));
    }
}