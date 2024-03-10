using MediatR;

namespace Darwin.Application.Common;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
