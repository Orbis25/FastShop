using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using OnlineShop.Data;
using Service.Commons;
using Service.Interface;
using Service.svc;
using Service.Svc;

namespace OnlineShop.ExtensionMethods
{
    public static class ServiceExtension
    {

        public static void AddConnection(this IServiceCollection service, IConfiguration configuration)
            => service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

        public static void AddIdentityExtension(this IServiceCollection service)
        {
            service.AddDefaultIdentity<ApplicationUser>()
                 .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>();
            service.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Lockout.AllowedForNewUsers = false;
            });
        }

        public static void PolicyCookies(this IServiceCollection service)
            => service.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



        public static void Services(this IServiceCollection service)
        {
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IAdminService, AdminService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IProductService, ProductService>();
            service.AddTransient<ICommon, Common>();
            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<IOffertService, OffertService>();
            service.AddTransient<ICouponService, CouponService>();
            service.AddTransient<ISaleService, SaleService>();
            service.AddTransient<IOrderService, OrderService>();

        }
    }
}
