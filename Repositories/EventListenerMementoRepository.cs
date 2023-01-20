using BackgroundProcessor.Memento;

namespace BackgroundProcessor.Repositories
{
    public class EventListenerMementoRepository
    {
        private readonly List<IMemento> mementos = new();

        public void Add(IMemento memento)
        {
            mementos.Add(memento);
        }

        public IMemento Get(Guid id)
        {
            return mementos.FirstOrDefault(x => x.EntityId == id);
        }
    }
}
