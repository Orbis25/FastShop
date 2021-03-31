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
        public CartItemService(ApplicationDbContext context) : base(context)
        {
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
    }
}
