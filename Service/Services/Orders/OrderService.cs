using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using OnlineShop.Data;
using Service.Commons;
using Service.Interface;

namespace Service.svc
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommon _common;
        public OrderService(ApplicationDbContext context , ICommon common)
        {
            _context = context;
            _common = common;
        }

        public async Task<bool> Add(Order model)
        {
            model.OrderCode = _common.GenerateCodeString(8).Replace("-", "X");
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Order> FindByOrderCode(string code, string userId) 
            => await _context.Orders.Include(x => x.Sale)
            .FirstOrDefaultAsync(x => x.OrderCode.Equals(code) && x.Sale.ApplicationUserId.Equals(userId));

        public async Task<IEnumerable<Order>> GetList() => await _context.Orders.ToListAsync();

        public async Task<Order> GetById(Guid id) => await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Remove(Guid id)
        {
            var model = await GetById(id);
            if(model == null) return false;
            model.State = Model.Enums.State.Deleted;
            _context.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Order entity)
        {
             var model = await GetById(entity.Id);
             model.UpdatedAt = DateTime.Now;
             model.Description = entity.Description;
             model.StateOrder = entity.StateOrder;
             _context.Update(model);
             return await _context.SaveChangesAsync() > 0;
        }
    }
}
