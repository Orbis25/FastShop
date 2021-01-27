using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderService : IBaseRepository<Order , Guid>
    {
        Task<Order> FindByOrderCode(string code, string userId);
        int OrderStatusPercent(StateOrder state);
    }
}
