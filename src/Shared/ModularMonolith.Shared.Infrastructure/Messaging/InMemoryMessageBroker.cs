using System.Threading.Tasks;
using ModularMonolith.Shared.Abstractions.Messaging;

namespace ModularMonolith.Shared.Infrastructure.Messaging
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        public Task PublishAsync(params IMessage[] messages)
        {
            throw new System.NotImplementedException();
        }
    }
}