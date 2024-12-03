using Social.Media.Post.CQRS.Core.Commands;

namespace Social.Media.Post.Command.Application.Commands.EditMessageCommand;

public sealed class EditMessageCommand : BaseCommand
{
    public string Message { get; set; }
}