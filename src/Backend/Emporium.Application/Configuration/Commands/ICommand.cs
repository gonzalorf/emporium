namespace Emporium.Application.Configuration.Commands;

public interface ICommand
{
    Guid CommandId { get; }
}

//public interface ICommand<out TResult> : ICommand
//{

//}