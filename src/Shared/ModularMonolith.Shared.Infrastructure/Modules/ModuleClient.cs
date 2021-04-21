using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ModularMonolith.Shared.Abstractions.Modules;

namespace ModularMonolith.Shared.Infrastructure.Modules
{
    internal sealed class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry _moduleRegistry;

        public ModuleClient(IModuleRegistry moduleRegistry)
        {
            _moduleRegistry = moduleRegistry;
        }

        public async Task PublishAsync(object message)
        {
            var key = message.GetType().Name;
            var registrations = _moduleRegistry
                .GetBroadcastRegistrations(key);

            var tasks = new List<Task>();
            
            foreach (var registration in registrations)
            {
                var action = registration.Action;
                var requestMessage = TranslateType(message, registration.RequestType);
                tasks.Add(action(requestMessage));
            }

            await Task.WhenAll(tasks);
        }

        private object TranslateType(object @object, Type type)
            => JsonSerializer.Deserialize(JsonSerializer.Serialize(@object), type);
    }
}