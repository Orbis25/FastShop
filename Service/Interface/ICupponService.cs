using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICupponService  : IRepository<Cupon , int>
    {
        Task<Cupon> GetByCupponCode(string code);
    }
}
