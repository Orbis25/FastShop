using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductService : IRepository<Product,Guid>
    {
        Task<bool> UplodadPic(ProductPic model);
        Task<IEnumerable<ProductPic>> ProductPics(Guid Id); 

    }
}
