using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Core.Interface;
using API.Core.Models;

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
                [Id],
                [ItemName],
                [IsCompleted],
                [CreatedDate],
                [CreatedBy]
            )
            VALUES
            (
                @Id,
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

    public async Task<Todo> GetById(string id)
    {
        string query = @"
            SELECT
                convert(nvarchar(50),  [Id]) Id,
                [ItemName],
                [IsCompleted],
                [CreatedDate],
                [CreatedBy]
            FROM [Todo]
            WHERE  convert(nvarchar(50),  [Id]) = @id AND IsDeleted = 0
        ";
        var result = await dbContext.Query<Todo>(query, new { id = id });
        return result.FirstOrDefault();
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
        WHERE CreatedBy = @user AND IsDeleted = 0
        ";
        var result = await dbContext.Query<Todo>(query, new { user = user });
        return result;
    }

    public async Task Update(Todo entity)
    {
        var query = @"
          UPDATE [Todo]
            SET  [ItemName] =@ItemName,
                    [IsCompleted] = @IsCompleted,
                    IsDeleted = @IsDeleted
            WHERE [Id] = @Id
        ";
        await dbContext.Execute(query, entity);
    }
}
