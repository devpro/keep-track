﻿using System;
using KeepTrack.Dal.MongoDb.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KeepTrack.InventoryComponent.Infrastructure.MongoDb.Entities
{
    public class Book : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("owner_id")]
        public string OwnerId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Series { get; set; }

        [BsonElement("finished_at")]
        public DateTime? FinishedAt { get; set; }
    }
}
