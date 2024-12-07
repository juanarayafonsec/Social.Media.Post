using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.Common.Events;

public sealed class PostLikedEvent() : BaseEvent(nameof(PostLikedEvent));