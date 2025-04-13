using Mediator;

namespace Emporium.Application.Configuration.Commands;
public abstract record CommandBase : ICommand, IRequest
{
    public Guid CommandId { get; }

    protected CommandBase()
    {
        CommandId = Guid.NewGuid();
    }

    protected CommandBase(Guid id)
    {
        CommandId = id;
    }
}

public abstract record CommandBase<TResult> : ICommand, IRequest<TResult>
{
    public Guid CommandId { get; }

    protected CommandBase()
    {
        CommandId = Guid.NewGuid();
    }

    protected CommandBase(Guid id)
    {
        CommandId = id;
    }
}