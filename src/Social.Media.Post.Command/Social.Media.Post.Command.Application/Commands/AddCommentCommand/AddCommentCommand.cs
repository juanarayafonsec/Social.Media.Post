using Social.Media.Post.CQRS.Core.Commands;

namespace Social.Media.Post.Command.Application.Commands.AddCommentCommand;

public sealed class AddCommentCommand : BaseCommand
{
    public string Comment { get; set; }
    public string Username { get; set; }
}