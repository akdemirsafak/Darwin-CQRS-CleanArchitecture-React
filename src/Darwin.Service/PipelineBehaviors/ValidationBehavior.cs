using Darwin.Core.BaseDto;
using FluentValidation;
using MediatR;

namespace Darwin.Service.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, DarwinResponse<TResponse>>
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest> _validators;

    public ValidationBehavior(IValidator<TRequest> validators)
    {
        _validators = validators;
    }

    public async Task<DarwinResponse<TResponse>> Handle(TRequest request, RequestHandlerDelegate<DarwinResponse<TResponse>> next, CancellationToken cancellationToken)
    {
        var validationResult= await _validators.ValidateAsync(request);
        if (!(validationResult.IsValid))
        {
            List<string> errors=new();
            validationResult.Errors.ForEach(validationResult => errors.Add(validationResult.ErrorMessage));
            return DarwinResponse<TResponse>.Fail(errors,400);
        }
        return await next();
    }
}
