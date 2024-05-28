using System;
using FluentValidation;

namespace API.Core;

public class GetTodoByIdValidator : AbstractValidator<GetTodoByIdCommand>
{
    public GetTodoByIdValidator()
    {
        RuleFor(x => x.TodoId).NotEmpty();
    }
}
