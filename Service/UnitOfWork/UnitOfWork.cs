using BussinesLayer.Interface.CartItems;
using BussinesLayer.Interface.Configurations;
using BussinesLayer.Interface.Countries;
using BussinesLayer.Interface.Emails;
using BussinesLayer.Interface.Emails.Templates;
using BussinesLayer.Interface.ImageServer;
using BussinesLayer.Interface.Products;
using BussinesLayer.Services.Cart;
using BussinesLayer.Services.Configurations;
using BussinesLayer.Services.Countries;
using BussinesLayer.Services.Emails;
using BussinesLayer.Services.Emails.Templates;
using BussinesLayer.Services.ImageServer;
using BussinesLayer.Services.Products;
using DataLayer.Settings.ImageServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Model.Models;
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
        private CategoryService _categoryService;
        private CouponService _couponService;
        private OffertService _offertService;
        private OrderService _orderService;
        private SaleService _saleService;
        private ProductService _productService;
        private ImageServerService _imageServerService;
        private CartItemService _cartItemService;
        private ConfigurationService _configurationService;
        private EmailService _emailService;
        private EmailTemplateService _emailTemplateService;
        private ReviewService _reviewService;
        private CityService _cityService;
        private CountryService _countryService;

        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Options
        private readonly IOptions<EmailSetting> _emailOptions;
        private readonly IOptions<InternalConfiguration> _internalOptions;
        private readonly IOptions<ImageServerOption> _imageServerOptions;

        #endregion

        public UnitOfWork(
            ApplicationDbContext dbContext,
            IOptions<EmailSetting> emailOptions,
            IOptions<InternalConfiguration> internalOptions,
            IOptions<ImageServerOption> imageServerOptions,
            UserManager<ApplicationUser> userManager)
        {
            _context = dbContext;
            _emailOptions = emailOptions;
            _internalOptions = internalOptions;
            _userManager = userManager;
            _imageServerOptions = imageServerOptions;

        }


        public IAccountService AccountService => _accountService ??= new AccountService(_context, _emailOptions, _internalOptions, _userManager);

        public IUserService UserService => _userService ??= new UserService(_context);


        public ICategoryService CategoryService => _categoryService ??= new CategoryService(_context);

        public ICouponService CouponService => _couponService ??= new CouponService(_context);

        public IOffertService OffertService => _offertService ??= new OffertService(_context);

        public IOrderService OrderService => _orderService ??= new OrderService(_context);

        public ISaleService SaleService => _saleService ??= new SaleService(_context, _emailOptions, _internalOptions);

        public IProductService ProductService => _productService ??= new ProductService(_context);

        public IImageServerService ImageServerService => _imageServerService ??= new ImageServerService(_imageServerOptions);

        public ICartItemService CartItemService => _cartItemService ??= new CartItemService(_context);

        public IConfigurationService ConfigurationService => _configurationService ??= new ConfigurationService(_context);

        public IEmailService EmailService => _emailService ??= new EmailService(_context, _emailOptions);

        public IEmailTemplateService EmailTemplateService => _emailTemplateService ??= new EmailTemplateService(_context);

        public IReviewService ReviewService => _reviewService ??= new ReviewService(_context);

        public ICountryService CountryService => _countryService ??= new CountryService(_context);

        public ICityService CityService => _cityService ??= new CityService(_context);
    }
}
