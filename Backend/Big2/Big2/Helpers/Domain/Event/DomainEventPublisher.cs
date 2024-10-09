namespace Big2.Helpers.Domain.Event
{
    public class DomainEventPublisher
    {
        // key : domainEvnet value : subscribers
        private static readonly Dictionary<Type, List<object>> _handlers = new();

        public static void Register<T>(IDomainEventSubscriber<T> eventSubscriber) where T : DomainEvent
        {
            var eventType = typeof(T);

            if (!_handlers.ContainsKey(eventType))
            {
                _handlers.Add(eventType, new List<object>());
                _handlers[eventType].Add(eventSubscriber);
            }
            else
            {
                _handlers[eventType].Add(eventSubscriber);
            }
        }

        public static void RaiseAsync<T>(T domainEvent) where T : DomainEvent
        {
            var eventType = domainEvent.GetType();

            if (_handlers.ContainsKey(eventType))
            {
                foreach (var handlerObject in _handlers[eventType])
                {
                    var handler = (IDomainEventSubscriber<T>)handlerObject;
                    handler.HandleAsync(domainEvent);
                }
            }
        }

        public static void Raise<T>(T domainEvent) where T : DomainEvent
        {
            var eventType = domainEvent.GetType();

            if (_handlers.ContainsKey(eventType))
            {
                foreach (var handlerObject in _handlers[eventType])
                {
                    var handler = (IDomainEventSubscriber<T>)handlerObject;
                    handler.Handle(domainEvent);
                }
            }
        }
    }
}
