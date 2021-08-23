using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ModularMonolith.Shared.Abstractions.Modules;

namespace ModularMonolith.Shared.Infrastructure.Modules
{
    internal sealed class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry _moduleRegistry;

        public ModuleClient(IModuleRegistry moduleRegistry)
            => _moduleRegistry = moduleRegistry;

        public async Task PublishAsync(object message)
        {
            var key = message.GetType().Name;
            var registrations = _moduleRegistry.GetBroadcastRegistration(key);

            var tasks = new List<Task>();
            
            foreach (var registration in registrations)
            {
                var handle = registration.Handle;
                var translatedMessage = TranslateType(message, registration.TargetType);
                tasks.Add(handle(translatedMessage));
            }

            await Task.WhenAll(tasks);
        }

        public static object TranslateType(object @object, Type targetType)
            => JsonSerializer.Deserialize(JsonSerializer.Serialize(@object), targetType);
    }
}