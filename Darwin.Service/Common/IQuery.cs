using MediatR;

namespace Darwin.Service.Common;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
