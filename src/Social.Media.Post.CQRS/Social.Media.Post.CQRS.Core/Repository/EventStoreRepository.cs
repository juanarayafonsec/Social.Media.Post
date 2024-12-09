using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Social.Media.Post.CQRS.Core.Config;
using Social.Media.Post.CQRS.Core.Domain;
using Social.Media.Post.CQRS.Core.Events;

namespace Social.Media.Post.CQRS.Core.Repository;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> _eventstoreCollection;

    public EventStoreRepository(IOptions<MongoDbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);
        _eventstoreCollection = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);
    }

    public async Task<List<EventModel>> FindByAggregateIdAsync(Guid aggregateId)
    {
        return await _eventstoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task SaveAsync(EventModel @event)
    {
        await _eventstoreCollection.InsertOneAsync(@event);
    }
}