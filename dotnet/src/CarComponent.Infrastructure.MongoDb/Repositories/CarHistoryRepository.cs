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
            var objectId = ParseObjectId(id);
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

        public async Task<CarHistoryModel> CreateAsync(CarHistoryModel model)
        {
            var collection = GetCollection<CarHistory>();
            var entity = Mapper.Map<CarHistory>(model);
            await collection.InsertOneAsync(entity);
            return Mapper.Map<CarHistoryModel>(entity);
        }

        public async Task<long> UpdateAsync(string id, CarHistoryModel model)
        {
            var objectId = ParseObjectId(id);
            var collection = GetCollection<CarHistory>();
            var entity = Mapper.Map<CarHistory>(model);
            var result = await collection.ReplaceOneAsync(x => x.Id == objectId, entity);
            return result.ModifiedCount;
        }

        public async Task<long> DeleteAsync(string id)
        {
            var objectId = ParseObjectId(id);
            var collection = GetCollection<CarHistory>();
            var result = await collection.DeleteOneAsync(x => x.Id == objectId);
            return result.DeletedCount;
        }

        private ObjectId ParseObjectId(string id, string message = null)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentNullException(nameof(id), $"{message}\"{id}\" is not a valid id.");
            }
            return objectId;
        }
    }
}
