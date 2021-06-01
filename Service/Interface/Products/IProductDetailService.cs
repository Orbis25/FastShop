using DataLayer.Models.Products;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.Products
{
    public interface IProductDetailService : IBaseRepository<ProductDetail, int>
    {
    }
}
