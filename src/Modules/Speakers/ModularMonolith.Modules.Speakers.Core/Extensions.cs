using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Modules.Speakers.Core.DAL.EF;
using ModularMonolith.Modules.Speakers.Core.DAL.Repositories;
using ModularMonolith.Modules.Speakers.Core.Services;
using ModularMonolith.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("ModularMonolith.Modules.Speakers.Api")]
namespace ModularMonolith.Modules.Speakers.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services
                .AddScoped<ISpeakersService, SpeakersService>()
                .AddScoped<ISpeakersRepository, SpeakersRepository>()
                .AddPostgres<SpeakersDbContext>();
    }
}