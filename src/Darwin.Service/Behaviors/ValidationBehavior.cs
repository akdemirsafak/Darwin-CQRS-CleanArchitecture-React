using FluentValidation;
using MediatR;

namespace Darwin.Service.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context=new ValidationContext<TRequest>(request);
        var failures=_validators
            .Select(validator => validator.Validate(context))
            .SelectMany(validationResult=>validationResult.Errors)
            .GroupBy(x=>x.ErrorMessage)
            .Select(x=>x.First())
            .Where(f=>f!=null)
            .ToList();
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
        return next();
    }
}