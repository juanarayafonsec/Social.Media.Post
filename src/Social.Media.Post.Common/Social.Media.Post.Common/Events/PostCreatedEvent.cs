using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.Common.Events;

public sealed class PostCreatedEvent() : BaseEvent(nameof(PostCreatedEvent))
{
    public string Author { get; set; }
    public string Message { get; set; }
    public DateTime DatePosted { get; set; }
}