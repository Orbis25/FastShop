using DataLayer.ViewModels.Products;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductService : IBaseRepository<Product, Guid>
    {
        Task<bool> UplodadPic(ProductPic model);
        Task<IEnumerable<ProductPic>> ProductPics(Guid Id);
        Task<List<Product>> GetHomeProducts(int take = 8);
        Task<ProductFilterVM> GetAllPaginateProducts(ProductFilterVM filters);
        Task<bool> RemoveProductPic(int id);
        Task<IEnumerable<Product>> GetSimilarItems(Guid id,int categoryId);
        Task<IEnumerable<ProductTopVM>> GetTopProduct(int take = 5);
    }
}
