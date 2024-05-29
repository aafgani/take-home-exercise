using System;
using System.Data;
using FluentValidation;

namespace API.Core;

public class AddTodosValidator : AbstractValidator<AddTodosCommand>
{
    public AddTodosValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.ItemName).NotEmpty();
        RuleFor(x => x.CreatedBy).NotEmpty();
    }
}
