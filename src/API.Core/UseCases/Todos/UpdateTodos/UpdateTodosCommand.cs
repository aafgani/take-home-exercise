using System;
using API.Core.Models;
using MediatR;

namespace API.Core;

public class UpdateTodosCommand : Todo, IRequest<EventResult<Todo>>
{

}

