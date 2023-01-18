using BackgroundProcessor.Events;

namespace BackgroundProcessor.EventProcessors
{
    public abstract class EventListener<T> where T : IEventPayload
    {
        protected virtual Event<T> Event { get; private set; } = null!;

        public void Load(T payload)
        {
            Event = new Event<T>(payload);
        }

        public abstract void Process();

        public Guid GetEventId()
        {
            return Event.Id;
        }
    }
}
