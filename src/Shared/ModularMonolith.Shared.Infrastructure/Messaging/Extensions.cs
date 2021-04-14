using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.Abstractions.Messaging;

namespace ModularMonolith.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();

            return services;
        }
    }
}