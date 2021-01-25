using BussinesLayer.Repository;
using DataLayer.Models.Categories;
using OnlineShop.Data;
using Service.Interface;

namespace Service.Svc
{
    public class CategoryService : BaseRepository<Category, ApplicationDbContext, int>, ICategoryService
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
