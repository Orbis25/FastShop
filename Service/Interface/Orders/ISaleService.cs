using DataLayer.Models.Cart;
using DataLayer.Utils.Paginations;
using DataLayer.ViewModels.Orders;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISaleService : IBaseRepository<Sale , Guid>
    {
        Task<bool> CreateSale(List<CartItem> items,Sale sale, string userEmail);
        Task<SaleFilterVM> GetSales(SaleFilterVM filters);
    }
}
