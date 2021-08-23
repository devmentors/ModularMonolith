using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.Abstractions.Events;
using ModularMonolith.Shared.Abstractions.Modules;

namespace ModularMonolith.Shared.Infrastructure.Modules
{
    internal static class Extensions
    {
        public static IServiceCollection AddModuleRequests(this IServiceCollection services)
        {
            services.AddModuleRegistry();
            services.AddSingleton<IModuleClient, ModuleClient>();
            return services;
        }

        public static void AddModuleRegistry(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var eventTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                .ToArray();

            services.AddSingleton<IModuleRegistry>(sp =>
            {
                var registry = new ModuleRegistry();
                var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();

                foreach (var eventType in eventTypes)
                {
                    var targetType = eventType;

                    var registration = new ModuleBroadcastRegistration(targetType, Handle);
                    registry.AddBroadcastRegistration(registration);
                    
                    Task Handle(object @event) =>
                        (Task) eventDispatcher.GetType()
                            .GetMethod(nameof(IEventDispatcher.PublishAsync))
                            ?.MakeGenericMethod(eventType)
                            .Invoke(eventDispatcher, new[] {@event});
                }

                return registry;
            });
        }
    }
}