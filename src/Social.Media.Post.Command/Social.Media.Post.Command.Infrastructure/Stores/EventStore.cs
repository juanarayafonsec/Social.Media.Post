using Social.Media.Post.Command.Domain.Aggregates;
using Social.Media.Post.CQRS.Core.Domain;
using Social.Media.Post.CQRS.Core.Events;
using Social.Media.Post.CQRS.Core.Exceptions;
using Social.Media.Post.CQRS.Core.Infrastructure;

namespace Social.Media.Post.Command.Infrastructure.Stores;

public class EventStore(IEventStoreRepository eventStoreRepository) : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository = eventStoreRepository;

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateIdAsync(aggregateId);

        if (eventStream == null || !eventStream.Any())
            throw new AggregateNotFoundException("Incorrect post ID provided!");
        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        var eventStream = await _eventStoreRepository.FindByAggregateIdAsync(aggregateId);

        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion) //eventStream[^1] last element
            throw new ConcurrencyException();

        var version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;
            var eventModel = new EventModel
            {
                TimeStamp = DateTime.UtcNow,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(PostAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };

            await _eventStoreRepository.SaveAsync(eventModel);
        }
    }
}