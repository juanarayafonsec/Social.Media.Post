using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.CQRS.Core.Domain;

public interface IEventStoreRepository
{
    Task<List<EventModel>> FindByAggregateIdAsync(Guid aggregateId);
    Task SaveAsync(EventModel @event);
}