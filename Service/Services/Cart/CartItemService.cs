using BussinesLayer.Interface.CartItems;
using BussinesLayer.Repository;
using DataLayer.Models.Cart;
using DataLayer.ViewModels.Cart;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Cart
{
    public class CartItemService : BaseRepository<CartItem, ApplicationDbContext, int>, ICartItemService
    {
        private readonly ApplicationDbContext _context;
        public CartItemService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetTotal(string userId)
        {
            return await base.GetAll(x => x.UserId == userId).CountAsync();
        }

        public async Task<bool> UpdateItem(CartItemUpdateVM model)
        {
            var exist = await base.GetById(model.Id);
            if (exist == null) return false;
            exist.Quantity = model.Quantity;
            return await base.Update(exist);
        }

        public async Task<bool> ClearCart(string userId)
        {
            var results = _context.CartItems.Where(x => x.UserId == userId);
            _context.RemoveRange(results);
            return await CommitAsync();
        }
    }
}
