using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBaseRepository<TEntity , Id> where TEntity : class where Id : struct
    {
        Task<bool> Add(TEntity model);
        Task<bool> Remove(Id id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Id id);
        Task<bool> Update(TEntity model);
    }
}
