namespace Big2.Helpers.Domain.Event
{
    public abstract class DomainEvent
    {
        public DateTime Created { get; private init; }

        public DomainEvent()
        {
            Created = DateTime.Now;
        }
    }
}
