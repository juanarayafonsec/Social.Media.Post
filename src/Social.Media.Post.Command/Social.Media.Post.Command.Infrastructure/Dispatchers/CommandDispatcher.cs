using Social.Media.Post.CQRS.Core.Commands;
using Social.Media.Post.CQRS.Core.Infrastructure;

namespace Social.Media.Post.Command.Infrastructure.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();

    public void Dispatch<TCommand>(Func<TCommand, Task> handler) where TCommand : BaseCommand
    {
        if (!_handlers.TryAdd(typeof(TCommand), x => handler((TCommand)x))) //parsing TCommand to concrete handler type 
            throw new InvalidOperationException($"Handler for '{typeof(TCommand).Name}' already registered.");
    }
    
    public async Task SendAsync(BaseCommand command)
    {
        var handler = _handlers.TryGetValue(command.GetType(), out var handlerType) 
            ? handlerType 
            : throw new ArgumentNullException(nameof(handlerType), "No command handler was registered");

        await handler(command);
    }

}