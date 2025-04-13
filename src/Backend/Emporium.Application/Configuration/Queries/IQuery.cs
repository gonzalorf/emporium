using MediatR;

namespace Emporium.Application.Configuration.Queries;
public interface IQuery<out TResult> : IRequest<TResult>
{
}