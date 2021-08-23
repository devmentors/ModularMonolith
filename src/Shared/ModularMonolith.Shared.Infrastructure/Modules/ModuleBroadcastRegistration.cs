using System;
using System.Threading.Tasks;

namespace ModularMonolith.Shared.Infrastructure.Modules
{
    public sealed class ModuleBroadcastRegistration
    {
        public Type TargetType { get; }
        public Func<object, Task> Handle { get; }
        public string Key => TargetType.Name;

        public ModuleBroadcastRegistration(Type targetType, Func<object, Task> handle)
        {
            TargetType = targetType;
            Handle = handle;
        }
    }
}