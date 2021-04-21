using System.Threading.Tasks;
using ModularMonolith.Shared.Abstractions.Messaging;

namespace ModularMonolith.Shared.Infrastructure.Messaging
{
    internal class AsyncMessageDispatcher : IAsyncMessageDispatcher
    {
        private readonly IMessageChannel _messageChannel;

        public AsyncMessageDispatcher(IMessageChannel messageChannel)
            => _messageChannel = messageChannel;

        public async Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
            => await _messageChannel.Writer.WriteAsync(message);
    }
}