using BackgroundProcessor;
using BackgroundProcessor.EventProcessors;
using BackgroundProcessor.Events;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<EventProcessorFactory>();
serviceCollection.AddScoped<IProxyGenerator, ProxyGenerator>();
serviceCollection.AddScoped<ProxyInterceptor>();

serviceCollection.AddTransient<PersonnelUpdatedEventListener>();

serviceCollection.AddSingleton<EventRepository>();
serviceCollection.AddSingleton<EventProcessorMementoRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var personnelUpdatedEvent = new PersonnelUpdatedEventPayload(Guid.NewGuid(), "darkoM", "Darko Meshkovski");

var eventProcessorFactory = serviceProvider.GetService<EventProcessorFactory>();
var personnelUpdatedEventProcessor = eventProcessorFactory.GetEventProcessor<PersonnelUpdatedEventListener, PersonnelUpdatedEventPayload>(personnelUpdatedEvent, true);

try
{
    personnelUpdatedEventProcessor.Process();
}
catch (Exception e)
{
    var eventProcessorMementoRepository = serviceProvider.GetService<EventProcessorMementoRepository>();
    var memento = personnelUpdatedEventProcessor.CreateMemento();
    eventProcessorMementoRepository.Add(memento);

    Console.WriteLine(e);
    throw;
}

Console.ReadKey();