using DataLayer.Models.Countries;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.Countries
{
    public interface ICountryService : IBaseRepository<AvaibleCountry,int>
    {
    }
}
