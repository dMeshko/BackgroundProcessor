using BackgroundProcessor.Repositories;
using System.Reflection;

namespace BackgroundProcessor.EventProcessor
{
    public class EventProcessor
    {
        private readonly EventRepository _eventRepository;
        private readonly EventListenerFactory _eventListenerFactory;
        private readonly EventListenerMementoRepository _eventListenerMementoRepository;

        public EventProcessor(EventRepository eventRepository, EventListenerFactory eventListenerFactory, EventListenerMementoRepository eventListenerMementoRepository)
        {
            _eventRepository = eventRepository;
            _eventListenerFactory = eventListenerFactory;
            _eventListenerMementoRepository = eventListenerMementoRepository;
        }

        public void Process()
        {
            var events = _eventRepository.GetAll();
            foreach (var @event in events)
            {
                //"BackgroundProcessor.Events.DocuQuest.DocumentUpdatedEventPayload"
                var eventPayloadType = Type.GetType(@event.PayloadType);
                var eventListeners = GetEventListeners(eventPayloadType);

                var payload = @event.Payload;

                var method = typeof(EventListenerFactory).GetMethod(nameof(EventListenerFactory.GetEventListener));
                foreach (var eventListener in eventListeners)
                {
                    var generic = method.MakeGenericMethod(eventListener, eventPayloadType);

                    var isDebugMode = false; //todo: FIGURE OUT A WAY TO INJECT THIS!!
                    var eventListenerHandle = (dynamic) generic.Invoke(_eventListenerFactory, new object[] { payload, isDebugMode });
                    try
                    {
                        eventListenerHandle.Process();
                    }
                    catch (Exception exception)
                    {
                        var memento = eventListenerHandle.CreateMemento();
                        _eventListenerMementoRepository.Add(memento); // addOrUpdate

                        Console.WriteLine(exception);
                    }
                }
            }
        }

        private IEnumerable<Type> GetEventListeners(Type eventPayloadType)
        {
            var executingAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();
            var eventListeners = executingAssemblyTypes.Where(x => !x.IsAbstract && !x.IsInterface)
                .Where(x => x.BaseType is { IsGenericType: true })
                .Where(x => x.BaseType!.GenericTypeArguments.Any() && x.BaseType.GenericTypeArguments.FirstOrDefault() == eventPayloadType);

            return eventListeners;
        }
    }
}
