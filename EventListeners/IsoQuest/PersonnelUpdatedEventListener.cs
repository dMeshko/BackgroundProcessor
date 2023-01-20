using BackgroundProcessor.EventProcessors;
using BackgroundProcessor.Events.IsoQuest;
using BackgroundProcessor.Memento;
using BackgroundProcessor.Repositories;

namespace BackgroundProcessor.EventListeners.IsoQuest
{
    public class PersonnelUpdatedEventListener : EventListener<PersonnelUpdatedEventPayload>, IEventListenerMemento
    {
        private readonly EventRepository _eventRepository;

        protected PersonnelUpdatedEventListener() { }

        public PersonnelUpdatedEventListener(EventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public virtual Guid PersonnelId { get; set; }

        public override void Process()
        {
            PersonnelId = Event.Payload.PersonnelId;

        }

        private class Memento : IMemento
        {
            public Memento(Guid entityId, Guid personnelId)
            {
                EntityId = entityId;
                PersonnelId = personnelId;
            }

            public Guid EntityId { get; }
            public Guid PersonnelId { get; }
        }

        public IMemento CreateMemento()
        {
            return new Memento(Event.Id, PersonnelId);
        }

        public void RestoreMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }
    }
}
