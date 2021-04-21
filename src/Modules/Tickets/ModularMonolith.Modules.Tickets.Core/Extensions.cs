using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Tickets.Core.Events.External;
using ModularMonolith.Modules.Tickets.Core.Events.External.Handlers;
using ModularMonolith.Shared.Abstractions.Events;
using ModularMonolith.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("ModularMonolith.Modules.Tickets.Api")]
namespace ModularMonolith.Modules.Tickets.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services;
    }
}