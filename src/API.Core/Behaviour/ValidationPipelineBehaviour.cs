using API.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Core.Behaviour
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var errors = _validators
               .Select(x => x.Validate(context))
               .SelectMany(x => x.Errors)
               .Where(x => x != null)
               .GroupBy(
               x => x.PropertyName,
               x => x.ErrorMessage,
               (propertyName, errorMessages) => new
               {
                   Key = propertyName,
                   Values = errorMessages.Distinct().ToArray()
               })
               .SelectMany(i => i.Values)
               .ToList();

            if (errors.Any())
            {
                return CreateValidationResult<TResponse>(errors);
            }

            return await next();
        }
        private TResult CreateValidationResult<TResult>(List<string> errors)
        {
            object result = typeof(EventResult<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(EventResult<TResult>.WithErrors))
                .Invoke(null, new object[] { errors })
                ;

            return (TResult)result;
        }
    }
}
