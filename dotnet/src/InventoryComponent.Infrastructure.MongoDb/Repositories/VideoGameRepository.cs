using AutoMapper;
using KeepTrack.Dal.MongoDb.Repositories;
using KeepTrack.InventoryComponent.Domain.Models;
using KeepTrack.InventoryComponent.Domain.Repositories;
using KeepTrack.InventoryComponent.Infrastructure.MongoDb.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Withywoods.Dal.MongoDb;

namespace KeepTrack.InventoryComponent.Infrastructure.MongoDb.Repositories
{
    public class VideoGameRepository : RepositoryBase<VideoGameModel, VideoGame>, IVideoGameRepository
    {
        public VideoGameRepository(IMongoDbContext mongoDbContext, ILogger<VideoGameRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "videogame";

        protected override FilterDefinition<VideoGame> GetFilter(string ownerId, string search, VideoGameModel input)
        {
            if (string.IsNullOrEmpty(search) && string.IsNullOrEmpty(input.State) && string.IsNullOrEmpty(input.Platform))
            {
                return base.GetFilter(ownerId, search, input);
            }

            var builder = Builders<VideoGame>.Filter;

            if (string.IsNullOrEmpty(input.State) && string.IsNullOrEmpty(input.Platform))
            {
                return builder.Eq(f => f.OwnerId, ownerId) & builder.Where(f => f.Title.ToLower().Contains(search.ToLower()));
            }

            var filter = builder.Eq(f => f.OwnerId, ownerId);
            if (!string.IsNullOrEmpty(search))
            {
                filter &= builder.Where(f => f.Title.ToLower().Contains(search.ToLower()));
            }

            if (!string.IsNullOrEmpty(input.State))
            {
                filter &= builder.Where(f => f.State == input.State);
            }

            if (!string.IsNullOrEmpty(input.Platform))
            {
                filter &= builder.Where(f => f.Platform == input.Platform);
            }

            return filter;
        }
    }
}
