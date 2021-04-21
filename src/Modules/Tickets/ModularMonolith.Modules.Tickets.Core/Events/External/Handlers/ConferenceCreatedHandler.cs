using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularMonolith.Shared.Abstractions.Events;

namespace ModularMonolith.Modules.Tickets.Core.Events.External.Handlers
{
    internal sealed class ConferenceCreatedHandler : IEventHandler<ConferenceCreated>
    {
        private readonly ILogger<ConferenceCreatedHandler> _logger;

        public ConferenceCreatedHandler(ILogger<ConferenceCreatedHandler> logger)
        {
            _logger = logger;
        }
        
        public async Task HandleAsync(ConferenceCreated @event)
        {
            // long-time running process
            await Task.Delay(10_000);
            _logger.LogInformation($"Received event about created conference with ID: {@event.Id}");
        }
    }
}