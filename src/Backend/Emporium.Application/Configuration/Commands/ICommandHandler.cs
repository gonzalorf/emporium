using MediatR;

namespace Emporium.Application.Configuration.Commands;
public interface ICommandHandler<in TCommand> :
    IRequestHandler<TCommand> where TCommand : IRequest
{

}

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
{

}