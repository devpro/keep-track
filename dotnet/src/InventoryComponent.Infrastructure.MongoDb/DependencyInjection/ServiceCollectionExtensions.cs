using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KeepTrack.InventoryComponent.Infrastructure.MongoDb.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInventoryInfrastructureMongoDb(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddTransient<Domain.Repositories.IBookRepository, Repositories.BookRepository>();
            services.TryAddTransient<Domain.Repositories.ITvShowRepository, Repositories.TvShowRepository>();
            services.TryAddTransient<Domain.Repositories.IVideoGameRepository, Repositories.VideoGameRepository>();

            return services;
        }
    }
}
