using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Conferences.Core;

[assembly:InternalsVisibleTo("ModularMonolith.Bootstrapper")]
namespace ModularMonolith.Modules.Conferences.Api
{
    internal static class ConferencesModule
    {
        public static IServiceCollection AddConferencesModule(this IServiceCollection services)
        {
            services.AddCore();
            
            return services;
        }

        public static IApplicationBuilder UseConferencesModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}