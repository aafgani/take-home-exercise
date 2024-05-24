using System;
using FluentValidation;

namespace API.Core;

public class GetTodosValidator : AbstractValidator<GetTodosCommand>
{
    public GetTodosValidator()
    {
        RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId must not be empty");
    }
}
