using Darwin.Core.BaseDto;
using FluentValidation;
using MediatR;

namespace Darwin.Service.PipelineBehavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, DarwinResponse<TResponse>>
    where TRequest : notnull
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<DarwinResponse<TResponse>> Handle(TRequest request, 
        RequestHandlerDelegate<DarwinResponse<TResponse>> next, 
        CancellationToken cancellationToken)
    {
        var validationResult= await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors= new List<string>();
            foreach (var error in validationResult.Errors)
                errors.Add(error.ErrorMessage);
            return DarwinResponse<TResponse>.Fail(errors,400);
        }
        return await next();
    }
}
