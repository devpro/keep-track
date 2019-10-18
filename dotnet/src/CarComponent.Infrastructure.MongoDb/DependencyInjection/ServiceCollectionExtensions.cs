using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KeepTrack.CarComponent.Infrastructure.MongoDb.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCarInfrastructureMongoDb(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddTransient<Domain.ICarRepository, Repositories.CarRepository>();
            services.TryAddTransient<Domain.ICarHistoryRepository, Repositories.CarHistoryRepository>();

            return services;
        }
    }
}
