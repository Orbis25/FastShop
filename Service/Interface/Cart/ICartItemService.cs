﻿using DataLayer.Models.Cart;
using DataLayer.ViewModels.Cart;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Interface.CartItems
{
    public interface ICartItemService : IBaseRepository<CartItem,int>
    {
        Task<int> GetTotal(string userId);
        Task<bool> UpdateItem(CartItemUpdateVM model);
        Task<bool> ClearCart(string userId);
    }

}