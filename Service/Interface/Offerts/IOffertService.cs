using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOffertService : IBaseRepository<Offert,int>
    {
        Task<bool> UploadImg(ImageOffert model);
        Task<Offert> GetActiveOffert();
    }
}
