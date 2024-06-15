using MediatR;

namespace Darwin.Contents.Service.Common;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
