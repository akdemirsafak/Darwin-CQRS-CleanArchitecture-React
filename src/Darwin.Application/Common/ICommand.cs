using MediatR;

namespace Darwin.Application.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
