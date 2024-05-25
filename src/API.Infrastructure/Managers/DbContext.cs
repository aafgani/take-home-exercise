using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using API.Core.Interface;
using Dapper;

namespace API.Infrastructure.Managers;

public class DbContext : IDbContext
{
    private IDbTransaction _transaction;
    private SqlConnection _dbConnection;

    public DbContext(SqlConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task Begin()
    {
        await _dbConnection.BeginTransactionAsync();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        // if (!this.disposed)
        // {
        //     if (disposing)
        //     {
        //         context.Dispose();
        //     }
        // }
        // this.disposed = true;

        if (_transaction != null)
            _transaction.Dispose();

        _transaction = null;

        if (_dbConnection != null)
            _dbConnection.Dispose();

        _dbConnection = null;
    }

    public async Task Execute(string query, object parameters, CommandType? cmdType = null)
    {
        await _dbConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);
    }

    public Task<IEnumerable<T>> Query<T>(string query, object parameters, CommandType? cmdType = null)
    {
        return _dbConnection.QueryAsync<T>(query, parameters, commandType: CommandType.Text);
    }

    public Task<Tuple<S, List<T>>> QueryMultiple<S, T>(string query, object parameters, CommandType? cmdType = null)
    {
        throw new NotImplementedException();
    }

    public Task Rollback()
    {
        throw new NotImplementedException();
    }
}
