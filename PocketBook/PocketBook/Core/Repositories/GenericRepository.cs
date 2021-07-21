using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PocketBook.Core.IRepositories;
using PocketBook.Data;

namespace PocketBook.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> 
        where T : class
    {
        internal readonly DbSet<T> DbSet;
        protected readonly ILogger Logger;

        protected GenericRepository(
            DbContext context,
            ILogger logger)
        {
            DbSet = context.Set<T>();
            Logger = logger;
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await DbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
