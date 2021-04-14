using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Tickets.Core;

[assembly:InternalsVisibleTo("ModularMonolith.Bootstrapper")]
namespace ModularMonolith.Modules.Tickets.Api
{
    internal static class TicketsModule
    {
        public static IServiceCollection AddTicketsModule(this IServiceCollection services)
        {
            services.AddCore();
            return services;
        }

        public static IApplicationBuilder UseTicketsModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}