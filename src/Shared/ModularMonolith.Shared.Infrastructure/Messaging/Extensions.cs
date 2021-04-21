using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.Abstractions.Messaging;

namespace ModularMonolith.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
            var options = services.GetOptions<MessagingOptions>("messaging");
            services.AddSingleton(options);

            if (options.UseBackgroundDispatcher)
            {
                services.AddHostedService<BackgroundDispatcher>();
            }
            
            return services;
        }
    }
}