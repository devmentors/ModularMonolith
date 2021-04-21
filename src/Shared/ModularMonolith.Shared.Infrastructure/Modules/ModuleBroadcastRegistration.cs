using System;
using System.Threading.Tasks;

namespace ModularMonolith.Shared.Infrastructure.Modules
{
    public sealed class ModuleBroadcastRegistration
    {
        public Type RequestType { get; }
        public Func<object, Task> Action { get; }
        public string Key => RequestType.Name;

        public ModuleBroadcastRegistration(Type requestType, Func<object, Task> action)
        {
            RequestType = requestType;
            Action = action;
        }
    }
}