using BackgroundProcessor.Events;
using BackgroundProcessor.Events.DocuQuest;
using BackgroundProcessor.Events.IsoQuest;

namespace BackgroundProcessor.Repositories
{
    public class EventRepository
    {
        private readonly List<Event<IEventPayload>> events = new()
        {
            new Event<IEventPayload>(new DocumentUpdatedEventPayload(Guid.NewGuid())),
            new Event<IEventPayload>(new PersonnelUpdatedEventPayload(Guid.NewGuid(), "darkoM", "Darko Meshkovski"))
        };

        public void Add(Event<IEventPayload> memento)
        {
            events.Add(memento);
        }

        public Event<IEventPayload> Get(Guid id)
        {
            return events.FirstOrDefault(x => x.Id == id);
        }

        public IList<Event<IEventPayload>> GetAll()
        {
            return events.OrderBy(x => x.CreatedAt).ToList();
        }

        public void Update(Event<IEventPayload> @event)
        {
            // do nothing - in memory repo
        }
    }
}
