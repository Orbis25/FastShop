using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderService : IRepository<Order , Guid>
    {
        Task<Order> FindByOrderCode(string code, string userId);
    }
}
