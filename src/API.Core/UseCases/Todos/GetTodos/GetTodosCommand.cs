using System;
using System.Collections.Generic;
using API.Core.Models;
using MediatR;

namespace API.Core;

public class GetTodosCommand : BasePaged, IRequest<EventResult<PagedResult<Todo>>>
{
    public string UserId { get; set; }
}
