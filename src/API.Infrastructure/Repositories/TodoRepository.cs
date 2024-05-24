using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core;
using API.Core.Interface;

namespace API.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IDbContext dbContext;

    public TodoRepository(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Add(Todo entity)
    {
        string query = @"
            INSERT INTO [Todo]
            (
                [ItemName],
                [IsCompleted],
                [CreatedDate],
                [CreatedBy]
            )
            VALUES
            (
                @ItemName
                ,@IsCompleted
                ,GETDATE()
                ,@CreatedBy
            )
        ";
        await dbContext.Execute(query, entity);
    }

    public Task Delete(Todo entity)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }

    public Task<IEnumerable<Todo>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Todo> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Todo>> GetByUser(string user)
    {
        string query = @"
        SELECT
            convert(nvarchar(50),  [Id]) Id,
            [ItemName],
            [IsCompleted],
            [CreatedDate],
            [CreatedBy]
        FROM [Todo]
        WHERE CreatedBy = @user
        ";
        var result = await dbContext.Query<Todo>(query, new { user = user });
        return result;
    }

    public Task UpdateAsync(Todo entity)
    {
        throw new NotImplementedException();
    }
}
