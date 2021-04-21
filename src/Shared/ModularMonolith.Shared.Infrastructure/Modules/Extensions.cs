using System;
using System.Linq;
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
            services.AddSingleton<IModuleClient, ModuleClient>();
            services.AddModuleRegistry();

            return services;
        }

        private static void AddModuleRegistry(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var eventTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                .ToArray();

            var registry = new ModuleRegistry();

            services.AddSingleton<IModuleRegistry>(sp =>
            {
                var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
                var eventDispatcherType = eventDispatcher.GetType();

                foreach (var eventType in eventTypes)
                {
                    registry.AddBroadcastRegistration(eventType, @event =>
                        (Task) eventDispatcherType
                            .GetMethod(nameof(IEventDispatcher.PublishAsync))
                            ?.MakeGenericMethod(eventType)
                            .Invoke(eventDispatcher, new[] {@event}));
                }

                return registry;
            });
        }
    }
}