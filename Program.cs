using BackgroundProcessor;
using BackgroundProcessor.EventListeners.DocuQuest;
using BackgroundProcessor.EventListeners.IsoQuest;
using BackgroundProcessor.EventProcessor;
using BackgroundProcessor.Events.IsoQuest;
using BackgroundProcessor.Repositories;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<EventProcessor>();
serviceCollection.AddSingleton<EventListenerFactory>();
serviceCollection.AddScoped<IProxyGenerator, ProxyGenerator>();
serviceCollection.AddScoped<ProxyInterceptor>();

serviceCollection.AddTransient<PersonnelUpdatedEventListener>();
serviceCollection.AddTransient<DocumentUpdatedEventListener>();
serviceCollection.AddTransient<BackgroundProcessor.EventListeners.PeopleQuest.DocumentUpdatedEventListener>();

serviceCollection.AddSingleton<EventRepository>();
serviceCollection.AddSingleton<EventListenerMementoRepository>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var eventProcessor = serviceProvider.GetService<EventProcessor>();
eventProcessor.Process();

Console.ReadKey();