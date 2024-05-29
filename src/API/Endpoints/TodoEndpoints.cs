using System;
using System.Text.RegularExpressions;
using API.Core;
using API.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
namespace API;

public static class TodoEndpoints
{
    public static RouteGroupBuilder MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/todos");

        group.MapGet("/", async (string userId, IMediator mediator) =>
        {
            try
            {
                var todos = await mediator.Send(new GetTodosCommand { UserId = userId });
                if (todos.IsError)
                    return Results.BadRequest(string.Join(",", todos.Error));

                return Results.Ok(todos.Result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        group.MapPost("/", async (Todo param, IMediator mediator) =>
        {
            try
            {
                var result = await mediator.Send(new AddTodosCommand { Id = Guid.NewGuid().ToString(), ItemName = param.ItemName, CreatedBy = param.CreatedBy, CreatedDate = DateTime.Now });
            }
            catch (Exception e)
            {
                throw;
            }
        });

        return group;
    }
}
