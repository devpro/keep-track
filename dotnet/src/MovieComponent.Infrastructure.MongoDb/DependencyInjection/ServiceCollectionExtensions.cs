using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KeepTrack.MovieComponent.Infrastructure.MongoDb.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMovieInfrastructureMongoDb(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddTransient<Domain.IMovieRepository, Repositories.MovieRepository>();

            return services;
        }
    }
}
