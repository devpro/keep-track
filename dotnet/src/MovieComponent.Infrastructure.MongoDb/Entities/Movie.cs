using KeepTrack.Dal.MongoDb.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KeepTrack.MovieComponent.Infrastructure.MongoDb.Entities
{
    public class Movie : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("owner_id")]
        public string OwnerId { get; set; }

        public string Title { get; set; }

        public int? Year { get; set; }

        public Imdb Imdb { get; set; }

        public Allocine Allocine { get; set; }
    }
}
