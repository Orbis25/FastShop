using Model.Models;
using Model.ViewModels;
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
        Task<List<Product>> GetHomeProducts(int take = 8);
        Task<ProductPaginationVM> GetAllPaginateProducts(int take = 9, int page = 1);
        Task<ProductPaginationVM> Filter(Filter filter);
    }
}
