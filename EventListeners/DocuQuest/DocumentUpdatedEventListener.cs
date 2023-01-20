using BackgroundProcessor.EventProcessors;
using BackgroundProcessor.Events.DocuQuest;
using BackgroundProcessor.Memento;

namespace BackgroundProcessor.EventListeners.DocuQuest
{
    public class DocumentUpdatedEventListener : EventListener<DocumentUpdatedEventPayload>, IEventListenerMemento
    {
        public DocumentUpdatedEventListener() { }

        public virtual Guid DocumentId { get; set; }

        public override void Process()
        {
            DocumentId = Event.Payload.DocumentId;

        }

        private class Memento : IMemento
        {
            public Memento(Guid entityId, Guid documentId)
            {
                EntityId = entityId;
                DocumentId = documentId;
            }

            public Guid EntityId { get; }
            public Guid DocumentId { get; }
        }

        public IMemento CreateMemento()
        {
            throw new NotImplementedException();
        }

        public void RestoreMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }
    }
}
