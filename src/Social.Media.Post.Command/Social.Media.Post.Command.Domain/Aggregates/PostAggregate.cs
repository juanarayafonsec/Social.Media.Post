﻿using Social.Media.Post.Common.Events;
using Social.Media.Post.CQRS.Core.Domain;

namespace Social.Media.Post.Command.Domain.Aggregates;

public class PostAggregate : AggregateRoot
{
    private bool _active;
    private string _author;
    private readonly Dictionary<Guid, Tuple<string, string>> _comments = new();

    public bool Active
    {
        get => _active;
        set => _active = value;
    }

    public PostAggregate()
    {
    }

    public PostAggregate(Guid id, string author, string message)
    {
        RaiseEvent(new PostCreatedEvent { Id = id, Author = author, Message = message, DatePosted = DateTime.UtcNow });
    }

    public void Apply(PostCreatedEvent @event)
    {
        _id = @event.Id;
        _active = true;
        _author = @event.Author;
    }

    public void EditMessage(string message)
    {
        IsActiveValidation("Cannot edit a message of an inactive post!");

        if (string.IsNullOrWhiteSpace(message))
            throw new InvalidOperationException(
                $"The value of {nameof(message)} cannot be null or empty. Please provide a valid {nameof(message)}!");

        RaiseEvent(new MessageUpdatedEvent { Id = _id, Message = message });
    }

    public void Apply(MessageUpdatedEvent @event)
    {
        _id = @event.Id;
    }

    public void LikePost()
    {
        IsActiveValidation("Cannot like an inactive post!");

        RaiseEvent(new PostLikedEvent { Id = _id });
    }

    public void Apply(PostLikedEvent @event)
    {
        _id = @event.Id;
    }

    public void AddComment(string comment, string username)
    {
        IsActiveValidation("Cannot add a comment to an inactive post!");

        if (string.IsNullOrWhiteSpace(comment))
            throw new InvalidOperationException(
                $"The value of {nameof(comment)} cannot be null or empty. Please provide a valid {nameof(comment)}!");

        RaiseEvent(new CommentAddedEvent
        {
            Id = _id,
            CommentId = Guid.NewGuid(),
            Comment = comment,
            Username = username,
            CommentDate = DateTime.UtcNow
        });
    }

    public void Apply(CommentAddedEvent @event)
    {
        _id = @event.Id;
        _comments.Add(@event.CommentId, new Tuple<string, string>(@event.Comment, @event.Username));
    }

    public void EditComment(Guid commentId, string comment, string username)
    {
        IsActiveValidation("Cannot edit a comment of an inactive post!");

        if (!_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidOperationException("You are not allowed to edit a comment that was made by another user!");

        RaiseEvent(new CommentUpdatedEvent
        {
            Id = _id,
            CommentId = commentId,
            Comment = comment,
            Username = username,
            EditDate = DateTime.UtcNow
        });
    }

    public void Apply(CommentUpdatedEvent @event)
    {
        _id = @event.Id;
        _comments[@event.CommentId] = new Tuple<string, string>(@event.Comment, @event.Username);
    }

    public void RemoveComment(Guid commentId, string username)
    {
        IsActiveValidation("You cannot remove a comment from an inactive post!");

        if (!_comments[commentId].Item2.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidOperationException(
                "You are not allowed to remove a comment that was made by another user!");

        RaiseEvent(new CommentRemovedEvent
        {
            Id = _id,
            CommentId = commentId
        });
    }

    public void Apply(CommentRemovedEvent @event)
    {
        _id = @event.Id;
        _comments.Remove(@event.CommentId);
    }

    public void DeletePost(string username)
    {
        IsActiveValidation("The post cannot be deleted because it has already been deleted!");

        if (!_author.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidOperationException("You are not allowed to delete a post that was made by someone else!");

        RaiseEvent(new PostRemovedEvent
        {
            Id = _id
        });
    }

    public void Apply(PostRemovedEvent @event)
    {
        _id = @event.Id;
        _active = false;
    }

    private void IsActiveValidation(string message)
    {
        if (!_active) throw new InvalidOperationException(message);
    }
}