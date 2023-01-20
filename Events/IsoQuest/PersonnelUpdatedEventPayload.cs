namespace BackgroundProcessor.Events.IsoQuest
{
    public class PersonnelUpdatedEventPayload : IEventPayload
    {
        public Guid PersonnelId { get; }
        public string Username { get; }
        public string FullName { get; }

        public PersonnelUpdatedEventPayload(Guid personnelId, string username, string fullName)
        {
            PersonnelId = personnelId;
            Username = username;
            FullName = fullName;
        }
    }
}
