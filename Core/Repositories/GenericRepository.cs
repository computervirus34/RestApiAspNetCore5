using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(
            ApplicationDbContext context,
            ILogger logger
            )
        {
            _context = context;
            _logger = logger;
            this._dbSet = context.Set<T>();
        }
        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual Task<bool> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetByID(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
