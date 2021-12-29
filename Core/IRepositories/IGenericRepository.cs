using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.Core.Repositories
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetByID(Guid Id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid Id);
        Task<bool> Update(T entity);
    }
}
