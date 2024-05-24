using System;
using API.Core;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
namespace API;

public static class TodoEndpoints
{
    public static RouteGroupBuilder MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/todos");

        group.MapGet("/", async (string userId, IMediator mediator) =>
        {
            var todos = await mediator.Send(new GetTodosCommand { UserId = userId });
            return todos is null ? Results.NotFound() : Results.Ok(todos.Result);
        });

        return group;
    }
}
