using System;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Models;
using MediatR;

namespace API.Core;

public class AddTodosHandler : IRequestHandler<AddTodosCommand, EventResult<Todo>>
{
    private readonly ITodoRepository _todoRepository;

    public AddTodosHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<EventResult<Todo>> Handle(AddTodosCommand request, CancellationToken cancellationToken)
    {
        await _todoRepository.Add(request);

        return new EventResult<Todo> { Result = request };
    }
}
