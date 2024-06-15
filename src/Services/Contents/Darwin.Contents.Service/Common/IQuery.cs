using MediatR;

namespace Darwin.Contents.Service.Common;
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
