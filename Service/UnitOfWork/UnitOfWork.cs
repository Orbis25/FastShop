using Microsoft.Extensions.Options;
using Model.Settings;
using OnlineShop.Data;
using Service.Interface;
using Service.svc;
using Service.Svc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Context
        private readonly ApplicationDbContext _context;
        #endregion

        #region Services
        private AccountService _accountService;
        private UserService _userService;
        private AdminService _adminService;
        private CategoryService _categoryService;
        private CouponService _couponService;
        private OffertService _offertService;
        private OrderService _orderService;
        private SaleService _saleService;
        private ProductService _productService;
        #endregion

        #region Options
        private readonly IOptions<EmailSetting> _emailOptions;
        private readonly IOptions<InternalConfiguration> _internalOptions;
        #endregion

        public UnitOfWork(
            ApplicationDbContext dbContext,
            IOptions<EmailSetting> emailOptions,
            IOptions<InternalConfiguration> internalOptions)
        {
            _context = dbContext;
            _emailOptions = emailOptions;
            _internalOptions = internalOptions;

        }


        public IAccountService AccountService => _accountService ??= new AccountService(_context, _emailOptions, _internalOptions);

        public IUserService UserService => _userService ??= new UserService(_context);

        public IAdminService AdminService => _adminService ??= new AdminService(_context);

        public ICategoryService CategoryService => _categoryService ??= new CategoryService(_context);

        public ICouponService CouponService => _couponService ??= new CouponService(_context);

        public IOffertService OffertService => _offertService ??= new OffertService(_context);

        public IOrderService OrderService => _orderService ??= new OrderService(_context);

        public ISaleService SaleService => _saleService ??= new SaleService(_context, _emailOptions, _internalOptions);

        public IProductService ProductService => _productService ??= new ProductService(_context);
    }
}
