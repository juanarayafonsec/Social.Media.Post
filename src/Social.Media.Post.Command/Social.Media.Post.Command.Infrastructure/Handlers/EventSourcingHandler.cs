using Social.Media.Post.Command.Domain.Aggregates;
using Social.Media.Post.CQRS.Core.Domain;
using Social.Media.Post.CQRS.Core.Handlers;
using Social.Media.Post.CQRS.Core.Infrastructure;

namespace Social.Media.Post.Command.Infrastructure.Handlers;

public class EventSourcingHandler(IEventStore eventStore) : IEventSourcingHandler<PostAggregate>
{
    public async Task<PostAggregate> GetByIdAsync(Guid id)
    {
        var aggregate = new PostAggregate();
        var events = await eventStore.GetEventsAsync(id);

        if (!events.Any()) return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
        aggregate.MarkChangesAsCommitted();
    }
}