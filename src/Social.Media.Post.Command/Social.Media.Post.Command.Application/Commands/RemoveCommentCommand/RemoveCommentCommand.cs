using Social.Media.Post.CQRS.Core.Commands;

namespace Social.Media.Post.Command.Application.Commands.RemoveCommentCOmmand;

public sealed class RemoveCommentCommand : BaseCommand
{
    public Guid CommentId { get; set; }
    public string Username { get; set; }
}