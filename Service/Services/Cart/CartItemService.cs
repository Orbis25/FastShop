using BussinesLayer.Interface.CartItems;
using BussinesLayer.Repository;
using DataLayer.Models.Cart;
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
    }
}
