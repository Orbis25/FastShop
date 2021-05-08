using BussinesLayer.Interface.Countries;
using BussinesLayer.Repository;
using DataLayer.Models.Countries;
using DataLayer.Utils.ResourcesClass;
using Newtonsoft.Json;
using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BussinesLayer.Services.Countries
{
    public class CityService : BaseRepository<AvaibleCity, ApplicationDbContext, int>, ICityService
    {
        private readonly ApplicationDbContext _dbContext;
        public CityService(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CitiesJson> GetCityRepository(string wwwRootPath, string countryCode)
        {
            try
            {
                var path = $@"{wwwRootPath}\\base\\Resources\\cities.json";
                using var r = new StreamReader(path);
                string json = r.ReadToEnd();
                var jsonList = JsonConvert.DeserializeObject<List<CitiesJson>>(json);
                return jsonList.Where(x => x.Country == countryCode).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
