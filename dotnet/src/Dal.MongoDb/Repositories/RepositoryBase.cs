using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using KeepTrack.Dal.MongoDb.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Withywoods.Dal.MongoDb;

namespace KeepTrack.Dal.MongoDb.Repositories
{
    /// <summary>
    /// MongoDB Data Access Layer repository abstract class.
    /// </summary>
    /// <typeparam name="T">Data Model class</typeparam>
    /// <typeparam name="U">Business class</typeparam>
    public abstract class RepositoryBase<T, U> : Withywoods.Dal.MongoDb.Repositories.RepositoryBase where U : IEntity
    {
        protected RepositoryBase(IMongoDbContext mongoDbContext, ILogger<RepositoryBase<T, U>> logger, IMapper mapper)
            : base(mongoDbContext, logger, mapper)
        {
        }

        protected virtual FilterDefinition<U> GetFilter(string ownerId, string search)
        {
            var builder = Builders<U>.Filter;
            if (string.IsNullOrEmpty(search))
            {
                return builder.Eq(f => f.OwnerId, ownerId);
            }

            return builder.Eq(f => f.OwnerId, ownerId) & builder.Text(search);
        }

        public async Task<T> FindOneAsync(string id, string ownerId)
        {
            var objectId = ParseObjectId(id);
            var collection = GetCollection<U>();
            var dbEntries = await collection.FindAsync(x => x.Id == objectId && x.OwnerId == ownerId);
            return Mapper.Map<T>(dbEntries.FirstOrDefault());
        }

        public async Task<List<T>> FindAllAsync(string ownerId, int page, int pageSize, string search)
        {
            var collection = GetCollection<U>();

            var dbEntries = await collection
                .Find(GetFilter(ownerId, search))
                .Skip(page)
                .Limit(pageSize)
                .ToListAsync();
            return Mapper.Map<List<T>>(dbEntries);
        }

        public async Task<T> CreateAsync(T model)
        {
            var collection = GetCollection<U>();
            var entity = Mapper.Map<U>(model);
            await collection.InsertOneAsync(entity);
            return Mapper.Map<T>(entity);
        }

        public async Task<long> UpdateAsync(string id, T model, string ownerId)
        {
            var objectId = ParseObjectId(id);
            var collection = GetCollection<U>();
            var entity = Mapper.Map<U>(model);
            var result = await collection.ReplaceOneAsync(x => x.Id == objectId && x.OwnerId == ownerId, entity);
            return result.ModifiedCount;
        }

        public async Task<long> DeleteAsync(string id, string ownerId)
        {
            var objectId = ParseObjectId(id);
            var collection = GetCollection<U>();
            var result = await collection.DeleteOneAsync(x => x.Id == objectId && x.OwnerId == ownerId);
            return result.DeletedCount;
        }

        protected static ObjectId ParseObjectId(string id, string message = null)
        {
            if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out var objectId))
            {
                throw new ArgumentException($"{message}{id} is not a valid id.", nameof(id));
            }

            return objectId;
        }
    }
}
