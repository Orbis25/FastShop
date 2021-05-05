﻿using BussinesLayer.Interface.CartItems;
using BussinesLayer.Interface.Configurations;
using BussinesLayer.Interface.Emails;
using BussinesLayer.Interface.Emails.Templates;
using BussinesLayer.Interface.ImageServer;
using BussinesLayer.Interface.Products;
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
        IImageServerService ImageServerService { get; }
        ICartItemService CartItemService { get; }
        IConfigurationService ConfigurationService { get; }
        IEmailService EmailService { get; }
        IEmailTemplateService EmailTemplateService { get; }
        IReviewService ReviewService { get; }

    }
}
