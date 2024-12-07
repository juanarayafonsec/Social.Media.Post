using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.Common.Events;

public sealed class PostRemovedEvent() : BaseEvent(nameof(PostRemovedEvent));