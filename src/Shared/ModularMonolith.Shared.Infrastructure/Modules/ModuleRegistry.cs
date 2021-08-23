using System;
using System.Collections.Generic;
using System.Linq;

namespace ModularMonolith.Shared.Infrastructure.Modules
{
    internal sealed class ModuleRegistry : IModuleRegistry
    {
        private readonly List<ModuleBroadcastRegistration> _broadcastRegistrations = new();

        public IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistration(string key)
            => _broadcastRegistrations.Where(x => x.Key == key);

        public void AddBroadcastRegistration(ModuleBroadcastRegistration registration)
        {
            if (registration is null)
            {
                throw new InvalidOperationException("Empty broadcast registration.");
            }

            if (string.IsNullOrWhiteSpace(registration.TargetType.Namespace))
            {
                throw new InvalidOperationException("Missing target type namespace");
            }
            
            _broadcastRegistrations.Add(registration);
        }
    }
}