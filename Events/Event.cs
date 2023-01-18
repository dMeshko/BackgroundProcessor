namespace BackgroundProcessor.Events
{
    public class Event<T> where T : IEventPayload
    {
        public Guid Id { get; }
        public T Payload { get; }
        public DateTime CreatedAt { get; }

        public Event(T payload)
        {
            Id = Guid.NewGuid(); //todo: assigned by the ORM
            Payload = payload;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
