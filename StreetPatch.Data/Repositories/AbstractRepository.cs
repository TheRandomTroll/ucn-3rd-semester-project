using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StreetPatch.Data.Entities.Base;
using StreetPatch.Data.Repositories.Interfaces;

namespace StreetPatch.Data.Repositories
{
    public abstract class AbstractRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : EntityBase
        where TContext : DbContext
    {
        private readonly TContext context;

        protected AbstractRepository(TContext context)
        {
            this.context = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if(entity.Id == default)
            {
                entity.Id = Guid.NewGuid();
            }

            context.Set<TEntity>().Add(entity);
            var rows = await context.SaveChangesAsync();
            return rows > 0 ? entity : default;
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == default(TEntity))
            {
                return default;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                entity.UpdatedOn = DateTime.Now;
                context.Entry(entity).State = EntityState.Modified;
                var rows = await context.SaveChangesAsync();
                return rows > 0 ? entity : default;
            }
            catch (Exception)
            {
                return default;
            }
        }

    }
}
