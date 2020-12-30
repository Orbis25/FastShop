using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBaseRepository<TEntity , Id> where TEntity : BaseModel<Id> where Id : struct
    {
        Task<bool> Add(TEntity model);
        Task<bool> Remove(Id id);
        Task<IEnumerable<TEntity>> GetList();
        Task<TEntity> GetById(Id id);
        Task<bool> Update(TEntity model);
    }
}
