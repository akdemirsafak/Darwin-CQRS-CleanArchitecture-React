using MediatR;

namespace Darwin.Service.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
