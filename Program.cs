using BackgroundProcessor;
using BackgroundProcessor.EventProcessors;
using BackgroundProcessor.Events;
using BackgroundProcessor.Repositories;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<EventListenerFactory>();
serviceCollection.AddScoped<IProxyGenerator, ProxyGenerator>();
serviceCollection.AddScoped<ProxyInterceptor>();

serviceCollection.AddTransient<PersonnelUpdatedEventListener>();

serviceCollection.AddSingleton<EventRepository>();
serviceCollection.AddSingleton<EventListenerMementoRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var personnelUpdatedEvent = new PersonnelUpdatedEventPayload(Guid.NewGuid(), "darkoM", "Darko Meshkovski");

var eventProcessorFactory = serviceProvider.GetService<EventListenerFactory>();
var personnelUpdatedEventProcessor = eventProcessorFactory.GetEventListener<PersonnelUpdatedEventListener, PersonnelUpdatedEventPayload>(personnelUpdatedEvent, true);

try
{
    personnelUpdatedEventProcessor.Process();
}
catch (Exception e)
{
    var eventProcessorMementoRepository = serviceProvider.GetService<EventListenerMementoRepository>();
    var memento = personnelUpdatedEventProcessor.CreateMemento();
    eventProcessorMementoRepository.Add(memento);

    Console.WriteLine(e);
    throw;
}

Console.ReadKey();