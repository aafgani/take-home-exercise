using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.Core.Interface;

public interface IDbContext : IDisposable
{
    Task Begin();
    Task<IEnumerable<T>> Query<T>(string query, object parameters, CommandType? cmdType = null);
    Task<Tuple<S, List<T>>> QueryMultiple<S, T>(string query, object parameters, CommandType? cmdType = null);
    Task Execute(string query, object parameters, CommandType? cmdType = null);
    void Commit();
    Task Rollback();
}
