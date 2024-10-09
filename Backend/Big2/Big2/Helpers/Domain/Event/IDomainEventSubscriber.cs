namespace Big2.Helpers.Domain.Event
{
    public interface IDomainEventSubscriber<T> where T : DomainEvent
    {
        Task HandleAsync(T domainEvent);

        Task Handle(T domainEvent);
    }
}
