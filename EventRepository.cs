using BackgroundProcessor.Events;

namespace BackgroundProcessor
{
    public class EventRepository
    {
        private readonly List<Event<IEventPayload>> events = new();

        public void Add(Event<IEventPayload> memento)
        {
            events.Add(memento);
        }

        public Event<IEventPayload> Get(Guid id)
        {
            return events.FirstOrDefault(x => x.Id == id);
        }
    }
}
