using BackgroundProcessor.EventProcessors;
using BackgroundProcessor.Events;
using Castle.DynamicProxy;

namespace BackgroundProcessor
{
    public class EventListenerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProxyInterceptor _proxyInterceptor;
        private readonly IProxyGenerator _proxyGenerator;

        public EventListenerFactory(IServiceProvider serviceProvider, ProxyInterceptor proxyInterceptor, IProxyGenerator proxyGenerator)
        {
            _serviceProvider = serviceProvider;
            _proxyInterceptor = proxyInterceptor;
            _proxyGenerator = proxyGenerator;
        }

        public TEventListener GetEventListener<TEventListener, TEventPayload>(TEventPayload payload, bool debugMode) where TEventListener : EventListener<TEventPayload> where TEventPayload : IEventPayload
        {
            TEventListener target;
            if (debugMode)
            {
                target = (TEventListener)_proxyGenerator.CreateClassProxy(typeof(TEventListener), _proxyInterceptor);
            }
            else
            {
                target = (TEventListener)_serviceProvider.GetService(typeof(TEventListener))!;
            }

            target.Load(payload);
            return target;
        }
    }
}
