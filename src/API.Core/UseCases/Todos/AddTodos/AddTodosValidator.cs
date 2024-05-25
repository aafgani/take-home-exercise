using System;
using FluentValidation;

namespace API.Core;

public class AddTodosValidator : AbstractValidator<AddTodosCommand>
{
    public AddTodosValidator()
    {
        RuleFor(x => x.ItemName).NotEmpty();
        RuleFor(x => x.CreatedBy).NotEmpty();
    }
}
