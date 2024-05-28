using System;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Models;
using MediatR;

namespace API.Core;

public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdCommand, EventResult<Todo>>
{
    private readonly ITodoRepository _todoRepository;

    public GetTodoByIdHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<EventResult<Todo>> Handle(GetTodoByIdCommand request, CancellationToken cancellationToken)
    {
        var dt = await _todoRepository.GetById(request.TodoId);
        return new EventResult<Todo> { Result = dt };
    }
}
