using System.Threading.Tasks;
using ModularMonolith.Shared.Abstractions.Messaging;

namespace ModularMonolith.Shared.Infrastructure.Messaging
{
    public interface IAsyncMessageDispatcher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}