using BussinesLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Models;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Threading.Tasks;

namespace Service.svc
{
    public class OrderService : BaseRepository<Order, ApplicationDbContext, Guid>, IOrderService
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> FindByOrderCode(string code, string userId)
            => await _context.Orders.Include(x => x.Sale)
            .FirstOrDefaultAsync(x => x.OrderCode.Equals(code) && x.Sale.ApplicationUserId.Equals(userId));

        public int OrderStatusPercent(StateOrder state)
        {
            return state switch
            {
                StateOrder.Storage => 25,
                StateOrder.Send => 50,
                StateOrder.Delivered => 100,
                _ => 100,
            };
        }
    }
}
