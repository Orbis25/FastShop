using BussinesLayer.Interface.Categories;
using BussinesLayer.Repository;
using DataLayer.Models.Categories;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Categories
{
    public class AdditionalFieldService : BaseRepository<AdditionalField, ApplicationDbContext, int>, IAdditionalFieldService
    {
        public AdditionalFieldService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
