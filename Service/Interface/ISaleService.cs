using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISaleService : IRepository<Sale , Guid>
    {
        Task<bool> CreateSale(Sale sale, string userEmail);
    }
}
