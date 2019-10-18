using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KeepTrack.CarComponent.Infrastructure.MongoDb.Entities
{
    public class Car
    {
        [BsonId]
        public string Id { get; set; }
        [BsonElement("commercial_name")]
        public string Name { get; set; }
    }
}
