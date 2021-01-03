using Service.Interface;

namespace BussinesLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAccountService AccountService { get; }
        IUserService UserService { get; }
        IAdminService AdminService { get; }
        ICategoryService CategoryService { get; }
        ICouponService CouponService { get; }
        IOffertService OffertService { get; }
        IOrderService OrderService { get; }
        ISaleService SaleService { get; }
        IProductService ProductService { get; }

    }
}
