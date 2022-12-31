using System;
using KeepTrack.Dal.MongoDb.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KeepTrack.InventoryComponent.Infrastructure.MongoDb.Entities
{
    public class VideoGame : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("owner_id")]
        public string OwnerId { get; set; }

        public string Title { get; set; }

        public string Platform { get; set; }

        [BsonElement("released_at")]
        public DateTime? ReleasedAt { get; set; }

        public string State { get; set; }

        [BsonElement("finished_at")]
        public DateTime? FinishedAt { get; set; }
    }
}
