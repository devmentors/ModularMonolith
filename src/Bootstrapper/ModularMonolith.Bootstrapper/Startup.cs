using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Conferences.Api;
using ModularMonolith.Modules.Speakers.Api;
using ModularMonolith.Modules.Tickets.Api;
using ModularMonolith.Shared.Infrastructure;

namespace ModularMonolith.Bootstrapper
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConferencesModule();
            services.AddSpeakersModule();
            services.AddTicketsModule();
            services.AddInfrastructure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseInfrastructure();
            app.UseRouting();
            app.UseConferencesModule();
            app.UseSpeakersModule();
            app.UseTicketsModule();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Modular Monolith API"));
            });
        }
    }
}
