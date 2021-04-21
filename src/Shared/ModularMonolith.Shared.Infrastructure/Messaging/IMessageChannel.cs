using System.Threading.Channels;
using ModularMonolith.Shared.Abstractions.Messaging;

namespace ModularMonolith.Shared.Infrastructure.Messaging
{
    public interface IMessageChannel
    {
        ChannelReader<IMessage> Reader { get; }
        ChannelWriter<IMessage> Writer { get; }
    }
}