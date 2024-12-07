﻿using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.Common.Events;

public sealed class CommentUpdatedEvent() : BaseEvent(nameof(CommentUpdatedEvent))
{
    public Guid CommentId { get; set; }
    public string Comment { get; set; }
    public string Username { get; set; }
    public DateTime EditDate { get; set; }
}