using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<List<BookModel>> FindAllAsync(string ownerId)
        {
            var collection = GetCollection<Book>();
            var dbEntries = await collection.FindAsync(x => x.OwnerId == ownerId);
            return Mapper.Map<List<BookModel>>(dbEntries.ToList());
        }
    }
}
