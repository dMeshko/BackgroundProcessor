using BackgroundProcessor.EventProcessors;
using BackgroundProcessor.Events;
using Castle.DynamicProxy;

namespace BackgroundProcessor
{
    public class EventProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProxyInterceptor _proxyInterceptor;
        private readonly IProxyGenerator _proxyGenerator;

        public EventProcessorFactory(IServiceProvider serviceProvider, ProxyInterceptor proxyInterceptor, IProxyGenerator proxyGenerator)
        {
            _serviceProvider = serviceProvider;
            _proxyInterceptor = proxyInterceptor;
            _proxyGenerator = proxyGenerator;
        }

        public T GetEventProcessor<T, U>(U payload, bool debugMode) where T : EventListener<U> where U : IEventPayload
        {
            var target = (T)_serviceProvider.GetService(typeof(T));
            target.Load(payload);

            if (!debugMode)
            {
                return target;
            }

            //var proxy = _proxyGenerator.CreateClassProxyWithTarget(typeof(T), target, _proxyInterceptor);
            var proxy = (T)_proxyGenerator.CreateClassProxy(typeof(T), _proxyInterceptor);
            proxy.Load(payload);

            return (T)proxy;
        }
    }
}
