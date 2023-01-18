namespace BackgroundProcessor.Memento
{
    /// <summary>
    /// Memento originator interface
    /// </summary>
    public interface IEventProcessorMemento
    {
        IMemento CreateMemento();
        void RestoreMemento(IMemento memento);
    }
}
