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
    public class TvShowRepository : RepositoryBase<TvShowModel, TvShow>, ITvShowRepository
    {
        public TvShowRepository(IMongoDbContext mongoDbContext, ILogger<TvShowRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "tvshow";

        protected override FilterDefinition<TvShow> GetFilter(string ownerId, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return base.GetFilter(ownerId, search);
            }

            var builder = Builders<TvShow>.Filter;
            return builder.Eq(f => f.OwnerId, ownerId) & builder.Where(f => f.Title.ToLower().Contains(search.ToLower()));
        }
    }
}
