using DataLayer.Models.Countries;
using DataLayer.Utils.ResourcesClass;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.Countries
{
    public interface ICityService : IBaseRepository<AvaibleCity, int>
    {
        List<CitiesJson> GetCityRepository(string wwwRootPath, string countryCode);
    }
}
