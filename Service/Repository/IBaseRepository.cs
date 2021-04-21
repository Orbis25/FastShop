using DataLayer.Models.Base;
using DataLayer.Utils.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBaseRepository<TEntity , TIdentifier> where TEntity : BaseModel<TIdentifier> where TIdentifier : IEquatable<TIdentifier>
    {
        Task<bool> Add(TEntity model);
        Task<bool> Remove(TIdentifier id);
        Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> filters = null, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filters = null, params Expression<Func<TEntity, object>>[] includes);
        Task<PaginationResult<TEntity>> GetAllPaginated(PaginationBase pagination,Expression<Func<TEntity, bool>> filters = null, Expression<Func<TEntity, bool>> relationsShipFilter = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetById(TIdentifier id, params Expression<Func<TEntity, object>>[] includes);
        Task<bool> Update(TEntity model);
        Task<bool> SoftRemove(TIdentifier id);
        Task<bool> Exist(TIdentifier id);
        Task<PaginationResult<TEntity>> CreatePagination(PaginationBase pagination, IQueryable<TEntity> result, Expression<Func<TEntity, bool>> relationsShipFilter = null);

    }
}
