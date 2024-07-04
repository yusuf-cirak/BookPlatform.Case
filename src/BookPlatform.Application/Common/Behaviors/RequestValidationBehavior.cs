using System.Text;
using FluentValidation;
using MediatR;

namespace BookPlatform.Application.Common.Behaviors;

public sealed class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationContext<object> context = new(request);

        // List<ValidationFailure> failures = _validators
        //     .Select(validator => validator.Validate(context))
        //     .SelectMany(result => result.Errors)
        //     .Where(failure => failure != null)
        //     .ToList();

        StringBuilder sb = new();

        _validators.Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToList()
            .ForEach(failure => sb.AppendLine(failure.ErrorMessage));


        string validationErrorMessage = sb.ToString();

        if (validationErrorMessage.Length > 0)
        {
            throw new ValidationException(validationErrorMessage);
        }

        return next();
    }
}