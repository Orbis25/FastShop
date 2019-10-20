using Model.ViewModels;
using OnlineShop.Data;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Svc
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context) => _context = context;
        public  IEnumerable<DashboardVM> GetAllCountServices()
            => new List<DashboardVM> {
                new DashboardVM  { Name = "Productos", Icon = "fas fa-box" , Quantity = _context.Products.Count() },
                new DashboardVM  { Name = "Usuarios", Icon = "fas fa-users" , Quantity = _context.ApplicationUsers.Count() },
                new DashboardVM  { Name = "Categorias", Icon = "fas fa-boxes" , Quantity = _context.Categories.Count() },
                new DashboardVM  { Name = "Cuppones Emitidos", Icon = "fas fa-ticket-alt" , Quantity = _context.Cupons.Count() },
                new DashboardVM  { Name = "Ventas", Icon = "fas fa-money-bill" , Quantity = _context.Sales.Count() },

            };
    }
}
