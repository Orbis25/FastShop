using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface ICategoryService : IRepository<Category , int>
    {
    }
}
