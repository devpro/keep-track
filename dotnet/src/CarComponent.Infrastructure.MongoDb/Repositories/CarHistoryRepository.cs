using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KeepTrack.CarComponent.Domain;
using KeepTrack.CarComponent.Infrastructure.MongoDb.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Withywoods.Dal.MongoDb;
using Withywoods.Dal.MongoDb.Repositories;

namespace KeepTrack.CarComponent.Infrastructure.Repositories
{
    public class CarHistoryRepository : RepositoryBase, ICarHistoryRepository
    {
        public CarHistoryRepository(IMongoDbContext mongoDbContext, ILogger<CarHistoryRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "car_history";

        public async Task<CarHistoryModel> FindOneAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentNullException(nameof(id), $"Cannot find the car history. \"{id}\" is not a valid id.");
            }

            var collection = GetCollection<CarHistory>();
            var dbEntries = await collection.FindAsync(x => x.Id == objectId);
            return Mapper.Map<CarHistoryModel>(dbEntries.FirstOrDefault());
        }

        public async Task<List<CarHistoryModel>> FindAllAsync(string carId)
        {
            var collection = GetCollection<CarHistory>();
            var dbEntries = await collection.FindAsync(x => x.CarId == carId);
            return Mapper.Map<List<CarHistoryModel>>(dbEntries.ToList());
        }
    }
}
