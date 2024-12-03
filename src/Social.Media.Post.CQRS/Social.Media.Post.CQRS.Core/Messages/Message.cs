namespace Social.Media.Post.CQRS.Core.Messages;

public abstract class Message
{
    public Guid Id { get; set; }
}