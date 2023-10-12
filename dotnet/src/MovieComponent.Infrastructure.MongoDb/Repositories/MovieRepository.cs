using AutoMapper;
using KeepTrack.Dal.MongoDb.Repositories;
using KeepTrack.MovieComponent.Domain;
using KeepTrack.MovieComponent.Infrastructure.MongoDb.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Withywoods.Dal.MongoDb;

namespace KeepTrack.MovieComponent.Infrastructure.MongoDb.Repositories
{
    public class MovieRepository : RepositoryBase<MovieModel, Movie>, IMovieRepository
    {
        public MovieRepository(IMongoDbContext mongoDbContext, ILogger<MovieRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "movie";

        protected override FilterDefinition<Movie> GetFilter(string ownerId, string search, MovieModel input)
        {
            if (string.IsNullOrEmpty(search))
            {
                return base.GetFilter(ownerId, search, input);
            }

            var builder = Builders<Movie>.Filter;
            return builder.Eq(f => f.OwnerId, ownerId) & builder.Where(f => f.Title.ToLower().Contains(search.ToLower()));
        }
    }
}
