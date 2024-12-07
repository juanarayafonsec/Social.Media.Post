using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.Common.Events;

public sealed class CommentRemovedEvent() : BaseEvent(nameof(CommentRemovedEvent))
{
    public Guid CommentId { get; set; }
}