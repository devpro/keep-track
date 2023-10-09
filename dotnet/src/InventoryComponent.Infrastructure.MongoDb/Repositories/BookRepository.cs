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
    public class BookRepository : RepositoryBase<BookModel, Book>, IBookRepository
    {
        public BookRepository(IMongoDbContext mongoDbContext, ILogger<BookRepository> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected override string CollectionName => "book";

        protected override FilterDefinition<Book> GetFilter(string ownerId, string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return base.GetFilter(ownerId, search);
            }

            var builder = Builders<Book>.Filter;
            return builder.Eq(f => f.OwnerId, ownerId) & builder.Where(f => f.Title.ToLower().Contains(search.ToLower()) || f.Series.ToLower().Contains(search.ToLower()) || f.Author.ToLower() == search.ToLower());
        }
    }
}
