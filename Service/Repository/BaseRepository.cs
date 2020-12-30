using Microsoft.EntityFrameworkCore;
using Model.Enums;
using Model.Models;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinesLayer.Repository
{
    public abstract class BaseRepository<TEntity,TContext, TId> : IBaseRepository<TEntity, TId> 
        where TEntity : BaseModel<TId> 
        where TId : struct
        where TContext : DbContext
    {

        private readonly TContext _context;
        public BaseRepository(TContext context) => _context = context;


        protected async Task<bool> CommitAsync()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await transaction.CommitAsync();
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }


        public async Task<bool> Add(TEntity model)
        {
            _context.Set<TEntity>().Add(model);
            return await CommitAsync();
        }

        public async Task<IEnumerable<TEntity>> GetList() => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetById(TId id) => await _context.Set<TEntity>().FindAsync(id);

        public async Task<bool> SoftRemove(TId id)
        {
            var result = await GetById(id);
            if (result == null) return false;
            result.State = State.Deleted;
            _context.Set<TEntity>().Update(result);
            return await CommitAsync();
        }

        public async Task<bool> Remove(TId id)
        {
            var result = await GetById(id);
            if (result == null) return false;
            _context.Set<TEntity>().Remove(result);
            return await CommitAsync();
        }

        public async Task<bool> Update(TEntity model)
        {
            var result = await GetById(model.Id);
            if (result == null) return false;
            _context.Set<TEntity>().Update(result);
            return await CommitAsync();

        }
    }
}
