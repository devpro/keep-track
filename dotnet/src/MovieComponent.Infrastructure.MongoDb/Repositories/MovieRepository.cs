using AutoMapper;
using KeepTrack.Dal.MongoDb.Repositories;
using KeepTrack.MovieComponent.Domain;
using KeepTrack.MovieComponent.Infrastructure.MongoDb.Entities;
using Microsoft.Extensions.Logging;
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
    }
}
