using Social.Media.Post.CQRS.Core.Messages;

namespace Social.Media.Post.CQRS.Core.Events;

public abstract class BaseEvent : Message
{
    protected BaseEvent(string type)
    {
        Type = type;
    }
    public int Version { get; set; }
    public string Type { get; private set; }
}