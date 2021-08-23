using System.Collections.Generic;

namespace ModularMonolith.Shared.Infrastructure.Modules
{
    public interface IModuleRegistry
    {
        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistration(string key);
        void AddBroadcastRegistration(ModuleBroadcastRegistration registration);
    }
}