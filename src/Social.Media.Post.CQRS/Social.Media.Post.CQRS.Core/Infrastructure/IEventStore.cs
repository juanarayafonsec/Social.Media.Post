using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.CQRS.Core.Infrastructure;

public interface IEventStore
{
    Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId); 
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);
}