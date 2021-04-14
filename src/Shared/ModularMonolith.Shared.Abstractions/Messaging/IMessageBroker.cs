using System.Threading.Tasks;

namespace ModularMonolith.Shared.Abstractions.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IMessage[] messages);
    }
}