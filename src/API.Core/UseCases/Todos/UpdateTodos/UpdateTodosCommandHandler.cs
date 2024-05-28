using System;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Models;
using MediatR;

namespace API.Core;

public class UpdateTodosCommandHandler : IRequestHandler<UpdateTodosCommand, EventResult<Todo>>
{
    private readonly ITodoRepository _todoRepository;

    public UpdateTodosCommandHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<EventResult<Todo>> Handle(UpdateTodosCommand request, CancellationToken cancellationToken)
    {
        await _todoRepository.Update(request);

        return new EventResult<Todo> { Result = request };
    }
}
