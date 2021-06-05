using BussinesLayer.Interface.Products;
using BussinesLayer.Repository;
using DataLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
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


        public override async Task<bool> Add(ProductDetail model)
        {
            //var result = GetAll(x => x.ProductId == model.ProductId && x.AdditionalFieldId == model.AdditionalFieldId);
            //if (result.Any())
            //{
            //    var _model = await result.FirstOrDefaultAsync();
            //    _model.Value = model.Value;
            //    return await base.Update(_model);
            //}
            return await base.Add(model);
        }
    }
}
