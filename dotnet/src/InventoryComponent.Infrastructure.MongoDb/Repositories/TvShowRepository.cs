using AutoMapper;
using KeepTrack.Dal.MongoDb.Repositories;
using KeepTrack.InventoryComponent.Domain.Models;
using KeepTrack.InventoryComponent.Domain.Repositories;
using KeepTrack.InventoryComponent.Infrastructure.MongoDb.Entities;
using Microsoft.Extensions.Logging;
using Withywoods.Dal.MongoDb;

namespace KeepTrack.InventoryComponent.Infrastructure.MongoDb.Repositories
{
    public class TvShowRepository : RepositoryBase<TvShowModel, TvShow>, IDataRepository<TvShowModel>, ITvShowRepository
    {
        public TvShowRepository(IMongoDbContext mongoDbContext, ILogger<TvShowRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "tvshow";
    }
}
