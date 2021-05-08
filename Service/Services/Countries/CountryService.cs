using BussinesLayer.Interface.Countries;
using BussinesLayer.Repository;
using DataLayer.Models.Countries;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Countries
{
    public class CountryService : BaseRepository<AvaibleCountry,ApplicationDbContext,int> , ICountryService
    {
        private readonly ApplicationDbContext _dbContext;
        public CountryService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
