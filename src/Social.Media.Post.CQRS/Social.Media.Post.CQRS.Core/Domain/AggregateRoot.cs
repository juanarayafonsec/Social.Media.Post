using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.CQRS.Core.Domain;

public abstract class AggregateRoot
{
    protected Guid _id;

    private readonly List<BaseEvent> _changes = new();

    public Guid Id => _id;

    public int Version { get; set; } = -1; //version ZERO is the base, -1 is unsigned

    public IEnumerable<BaseEvent> GetUncommittedChanges() => _changes;

    public void MarkChangesAsCommitted() => _changes.Clear();

    private void ApplyChange(BaseEvent @event, bool isNew)
    {
        //try to get Apply method from the concrete aggregate with reflection
        var method = GetType().GetMethod("Apply", [@event.GetType()]);

        if (method is null)
            throw new ArgumentNullException(nameof(@event),
                $"The Apply method was not found in the aggregate for {@event.GetType().Name}");

        method.Invoke(this, [@event]);
        
        if (isNew) _changes.Add(@event);
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChange(@event, true);
    }

    public void ReplayEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChange(@event, false);
        }
    }
}