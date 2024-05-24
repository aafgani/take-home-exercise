using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Core;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(string id);
    Task<IEnumerable<T>> GetAll();
    Task Add(T entity);
    Task Delete(T entity);
    Task UpdateAsync(T entity);
}
