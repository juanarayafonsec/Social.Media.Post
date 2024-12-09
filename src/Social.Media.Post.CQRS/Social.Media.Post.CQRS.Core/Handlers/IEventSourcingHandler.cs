using Social.Media.Post.CQRS.Core.Domain;

namespace Social.Media.Post.CQRS.Core.Handlers;

public interface IEventSourcingHandler<T>
{
    Task SaveAsync(AggregateRoot aggregate);
    Task<T> GetByIdAsync(Guid id);
}