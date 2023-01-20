using BackgroundProcessor.Repositories;
using Castle.DynamicProxy;

namespace BackgroundProcessor
{
    public class ProxyInterceptor : IInterceptor
    {
        private readonly EventListenerMementoRepository _eventListenerMementoRepository;

        public ProxyInterceptor(EventListenerMementoRepository eventListenerMementoRepository)
        {
            _eventListenerMementoRepository = eventListenerMementoRepository;
        }

        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            if (methodName.Contains("set_"))
            {
                var target = invocation.InvocationTarget;
                var eventId = (target as dynamic).GetEventId();

                var memento = _eventListenerMementoRepository.Get(eventId);
                var overridenValue = memento.GetType().GetProperty(methodName.Replace("set_", "")).GetValue(memento, null);
                
                var invocationArguments = invocation.Arguments;
                invocationArguments[0] = overridenValue;
            }

            invocation.Proceed();
        }
    }
}
