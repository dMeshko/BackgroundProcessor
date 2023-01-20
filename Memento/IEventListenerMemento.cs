namespace BackgroundProcessor.Memento
{
    /// <summary>
    /// Memento originator interface
    /// </summary>
    public interface IEventListenerMemento
    {
        IMemento CreateMemento();
        void RestoreMemento(IMemento memento);
    }
}
