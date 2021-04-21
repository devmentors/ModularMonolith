using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularMonolith.Shared.Infrastructure.Modules
{
    public interface IModuleRegistry
    {
        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
        void AddBroadcastRegistration(Type requestType, Func<object, Task> action);
    }
}