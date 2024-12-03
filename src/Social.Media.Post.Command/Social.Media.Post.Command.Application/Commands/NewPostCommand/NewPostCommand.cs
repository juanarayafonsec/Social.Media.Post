using Social.Media.Post.CQRS.Core.Commands;

namespace Social.Media.Post.Command.Application.Commands.NewPostCommand;

public sealed class NewPostCommand : BaseCommand
{
    public string Author { get; set; }
    public string Message { get; set; }
}