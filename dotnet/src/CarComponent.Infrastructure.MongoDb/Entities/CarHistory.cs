using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KeepTrack.CarComponent.Infrastructure.MongoDb.Entities
{
    public class CarHistory
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("car_id")]
        public string CarId { get; set; }
        [BsonElement("history_date")]
        public DateTime HistoryDate { get; set; }
    }
}
