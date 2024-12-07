using Social.Media.Post.CQRS.Core.Commands;

namespace Social.Media.Post.CQRS.Core.Infrastructure;

public interface ICommandDispatcher
{
    void Dispatch<TCommand>(Func<TCommand, Task> command) where TCommand : BaseCommand;
    Task SendAsync(BaseCommand command);
}