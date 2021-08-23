using System.Threading.Tasks;

namespace ModularMonolith.Shared.Abstractions.Modules
{
    public interface IModuleClient
    {
        Task PublishAsync(object message);
    }
}