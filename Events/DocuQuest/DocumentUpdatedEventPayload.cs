namespace BackgroundProcessor.Events.DocuQuest
{
    public class DocumentUpdatedEventPayload : IEventPayload
    {
        public Guid DocumentId { get; }

        public DocumentUpdatedEventPayload(Guid documentId)
        {
            DocumentId = documentId;
        }
    }
}
