using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KeepTrack.CarComponent.Domain;
using KeepTrack.CarComponent.Infrastructure.MongoDb.Entities;
using KeepTrack.Dal.MongoDb.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Withywoods.Dal.MongoDb;

namespace KeepTrack.CarComponent.Infrastructure.Repositories
{
    public class CarHistoryRepository : RepositoryBase<CarHistoryModel, CarHistory>, ICarHistoryRepository
    {
        public CarHistoryRepository(IMongoDbContext mongoDbContext, ILogger<CarHistoryRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "car_history";

        public async Task<List<CarHistoryModel>> FindAllAsync(string carId, string ownerId)
        {
            var collection = GetCollection<CarHistory>();
            var dbEntries = await collection.FindAsync(x => x.CarId == carId && x.OwnerId == ownerId);
            return Mapper.Map<List<CarHistoryModel>>(dbEntries.ToList());
        }
    }
}
