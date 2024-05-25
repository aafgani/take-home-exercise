using System;
using API.Core.Models;
using MediatR;

namespace API.Core;

public class AddTodosCommand : Todo, IRequest<EventResult<Todo>>
{

}
