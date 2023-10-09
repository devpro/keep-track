﻿using AutoMapper;
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

        protected override FilterDefinition<VideoGame> GetFilter(string ownerId, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return base.GetFilter(ownerId, search);
            }

            var builder = Builders<VideoGame>.Filter;
            return builder.Eq(f => f.OwnerId, ownerId) & builder.Where(f => f.Title.ToLower().Contains(search.ToLower()));
        }
    }
}
