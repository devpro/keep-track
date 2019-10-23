﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KeepTrack.CarComponent.Infrastructure.MongoDb.Entities
{
    public class CarHistoryFuel
    {
        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("volume")]
        public double? Volume { get; set; }

        [BsonElement("unit_price")]
        public double? UnitPrice { get; set; }

        [BsonElement("amount")]
        public double? Amount { get; set; }

        [BsonElement("is_full_tank")]
        public bool? IsFullTank { get; set; }

        [BsonElement("delta_mileage")]
        public double? DeltaMileage { get; set; }

        [BsonElement("last_refuel_history_id")]
        public ObjectId? LastRefuelHistoryId { get; set; }
    }
}
