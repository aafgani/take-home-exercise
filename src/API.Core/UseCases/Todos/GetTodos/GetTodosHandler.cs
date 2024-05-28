using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Core.Models;
using MediatR;

namespace API.Core;

public class GetTodosHandler : IRequestHandler<GetTodosCommand, EventResult<PagedResult<Todo>>>
{
    private readonly ITodoRepository _repository;

    public GetTodosHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<EventResult<PagedResult<Todo>>> Handle(GetTodosCommand request, CancellationToken cancellationToken)
    {
        return new EventResult<PagedResult<Todo>>
        {
            Result = new PagedResult<Todo>
            {
                Data = await _repository.GetByUser(request.UserId)
            }
        };
    }
}
