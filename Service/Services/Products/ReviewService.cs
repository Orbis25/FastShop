using BussinesLayer.Interface.Products;
using BussinesLayer.Repository;
using DataLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Products
{
    public class ReviewService : BaseRepository<Review, ApplicationDbContext, int>, IReviewService
    {
        private readonly ApplicationDbContext _dbContext;
        public ReviewService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistReview(Guid productId, string userId)
        {
            var result = await _dbContext.Reviews.AnyAsync(x => x.ProductId == productId && x.UserId == userId);
            return result;
        }

        public decimal GetAverage(Guid productId)
        {
            var total = _dbContext.Reviews.Where(x => x.ProductId == productId).AsNoTracking().Sum(x => x.Rating);
            var count = _dbContext.Reviews.AsNoTracking().Count(x => x.ProductId == productId);
            if (count == 0) return 0;
            return Math.Round((total / count), 2);
        }
    }
}
