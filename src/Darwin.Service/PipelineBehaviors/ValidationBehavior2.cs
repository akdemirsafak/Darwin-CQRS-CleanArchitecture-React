using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Darwin.Service.PipelineBehaviors;

public class ValidationBehavior2<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior2(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {

        var context=new ValidationContext(request);
        var failures=_validators.Select(x=>x.Validate(request)).SelectMany(x=>x.Errors).Where(x=>x!=null).ToList();

        if (failures.Any())
        {
            throw new FluentValidation.ValidationException(failures);
        }
        return next();
    }
}
