using DataLayer.Models.Base;
using DataLayer.Utils.Paginations;
using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BussinesLayer.Repository
{
    public abstract class BaseRepository<TEntity, TContext, TIdentifier> : IBaseRepository<TEntity, TIdentifier>
        where TEntity : BaseModel<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
        where TContext : DbContext
    {

        private readonly TContext _context;
        public BaseRepository(TContext context) => _context = context;

        protected async Task<bool> CommitAsync()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var result = await _context.SaveChangesAsync() > 0;
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public virtual async Task<bool> Add(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
            return await CommitAsync();
        }

        public virtual async Task<bool> Remove(TIdentifier id)
        {
            var result = await GetById(id);
            if (result == null) return false;
            _context.Set<TEntity>().Remove(result);
            return await CommitAsync();
        }

        public virtual async Task<bool> Update(TEntity model)
        {

            if (!await Exist(model.Id)) return false;
            _context.Set<TEntity>().Update(model);
            return await CommitAsync();

        }


        public virtual async Task<bool> SoftRemove(TIdentifier id)
        {
            var result = await GetById(id);
            if (result == null) return false;
            result.State = State.Deleted;
            _context.Set<TEntity>().Update(result);
            return await CommitAsync();
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filters = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var results = _context.Set<TEntity>().AsQueryable();
            foreach (var include in includes) results = results.Include(include);
            if (filters == null) return results;
            return results.Where(filters);
        }

        public virtual async Task<PaginationResult<TEntity>> GetAllPaginated(PaginationBase pagination,
            Expression<Func<TEntity, bool>> filters = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var result = GetAll(filters, includes);
            var total = result.Count();
            var pages = total / pagination.Qyt;
            result = result.Skip((pagination.Page - 1) * pagination.Qyt).Take(pagination.Qyt);

            return new()
            {
                Pages = pages,
                Total = total,
                Qyt = pagination.Qyt,
                ActualPage = pagination.Page,
                Results = await result.ToListAsync()
            };
        }

        public virtual async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> filters = null, params Expression<Func<TEntity, object>>[] includes)
        {
            return await GetAll(filters, includes).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(TIdentifier id, params Expression<Func<TEntity, object>>[] includes)
           => await GetAll(null, includes).FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<bool> Exist(TIdentifier id) => await _context.Set<TEntity>().AnyAsync(x => x.Id.Equals(id));
    }
}
