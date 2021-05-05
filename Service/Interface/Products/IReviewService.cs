using DataLayer.Models.Products;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.Products
{
    public interface IReviewService : IBaseRepository<Review,int>
    {
        decimal GetAverage(Guid productId);
        Task<bool> ExistReview(Guid productId, string userId);
       
    }
}
