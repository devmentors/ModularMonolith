using System.Linq;
using System.Threading.Tasks;
using ModularMonolith.Shared.Abstractions.Messaging;
using ModularMonolith.Shared.Abstractions.Modules;

namespace ModularMonolith.Shared.Infrastructure.Messaging
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;
        private readonly MessagingOptions _messagingOptions;
        private readonly IAsyncMessageDispatcher _asyncMessageDispatcher;

        public InMemoryMessageBroker(IModuleClient moduleClient, MessagingOptions messagingOptions, 
            IAsyncMessageDispatcher asyncMessageDispatcher)
        {
            _moduleClient = moduleClient;
            _messagingOptions = messagingOptions;
            _asyncMessageDispatcher = asyncMessageDispatcher;
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
                    ? _asyncMessageDispatcher.PublishAsync(x) 
                    : _moduleClient.PublishAsync(x));

            await Task.WhenAll(tasks);
        }
    }
}