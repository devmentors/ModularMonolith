using System.Linq;
using System.Threading.Tasks;
using ModularMonolith.Shared.Abstractions.Messaging;
using ModularMonolith.Shared.Abstractions.Modules;

namespace ModularMonolith.Shared.Infrastructure.Messaging
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;
        private readonly IAsynchronousDispatcher _asynchronousDispatcher;
        private readonly MessagingOptions _messagingOptions;

        public InMemoryMessageBroker(IModuleClient moduleClient, MessagingOptions messagingOptions, 
            IAsynchronousDispatcher asynchronousDispatcher)
        {
            _moduleClient = moduleClient;
            _messagingOptions = messagingOptions;
            _asynchronousDispatcher = asynchronousDispatcher;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
            {
                return;
            }

            var tasks = messages.Select(x => _messagingOptions.UseBackgroundDispatcher
            ? _asynchronousDispatcher.PublishAsync(x)
            : _moduleClient.PublishAsync(x));
            await Task.WhenAll(tasks);
        }
    }
}