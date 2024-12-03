using Social.Media.Post.CQRS.Core.Commands;

namespace Social.Media.Post.Command.Application.Commands.DeletePostCommand;

public sealed class DeletePostCommand : BaseCommand
{
    public string Username { get; set; }
}