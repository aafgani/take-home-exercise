using System;
using API.Core.Models;
using MediatR;

namespace API.Core;

public class GetTodoByIdCommand : IRequest<EventResult<Todo>>
{
    public string TodoId { get; set; }
}
