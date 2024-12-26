using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Abstractions
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
    }
}
