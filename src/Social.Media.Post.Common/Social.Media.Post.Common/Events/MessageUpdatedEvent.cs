using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.Common.Events;

public sealed class MessageUpdatedEvent() : BaseEvent(nameof(MessageUpdatedEvent))
{
    public string Message { get; set; }
}