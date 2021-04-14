using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Speakers.Core;

[assembly:InternalsVisibleTo("ModularMonolith.Bootstrapper")]
namespace ModularMonolith.Modules.Speakers.Api
{
    internal static class SpeakersModule
    {
        public static IServiceCollection AddSpeakersModule(this IServiceCollection services)
        {
            services.AddCore();
            
            return services;
        }

        public static IApplicationBuilder UseSpeakersModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}