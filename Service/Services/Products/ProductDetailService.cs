using BussinesLayer.Interface.Products;
using BussinesLayer.Repository;
using DataLayer.Models.Products;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Products
{
    public class ProductDetailService : BaseRepository<ProductDetail, ApplicationDbContext, int>, IProductDetailService
    {
        public ProductDetailService(ApplicationDbContext context) : base(context)
        {

        }
    }
}
