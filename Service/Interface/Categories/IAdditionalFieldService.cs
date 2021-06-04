using DataLayer.Models.Categories;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.Categories
{
    public interface IAdditionalFieldService : IBaseRepository<AdditionalField, int>
    {
    }
}
