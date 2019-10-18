using System;
using System.Threading.Tasks;
using AutoMapper;
using KeepTrack.CarComponent.Domain;
using KeepTrack.CarComponent.Infrastructure.MongoDb.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Withywoods.Dal.MongoDb;
using Withywoods.Dal.MongoDb.Repositories;

namespace KeepTrack.CarComponent.Infrastructure.Repositories
{
    public class CarRepository : RepositoryBase, ICarRepository
    {
        public CarRepository(IMongoDbContext mongoDbContext, ILogger<CarRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "car";

        public async Task<CarModel> FindOneAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), $"Cannot find a car. \"{id}\" is not a valid id.");
            }

            var collection = GetCollection<Car>();
            var dbEntries = await collection.FindAsync(x => x.Id == id);
            return Mapper.Map<CarModel>(dbEntries.FirstOrDefault());
        }
    }
}
