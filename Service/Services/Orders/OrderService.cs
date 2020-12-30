using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BussinesLayer.Repository;
using DataLayer.Utils.Paginations;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Commons;
using Service.Interface;

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
    }
}
