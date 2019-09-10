using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IProductService : IRepository<Product,Guid>
    {
    }
}
