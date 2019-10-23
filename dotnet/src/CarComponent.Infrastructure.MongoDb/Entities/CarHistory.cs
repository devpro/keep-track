using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KeepTrack.CarComponent.Infrastructure.MongoDb.Entities
{
    public partial class CarHistory
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("owner_id")]
        public string OwnerId { get; set; }

        [BsonElement("car_id")]
        public string CarId { get; set; }

        [BsonElement("history_date")]
        public DateTime HistoryDate { get; set; }

        [BsonElement("mileage")]
        public double Mileage { get; set; }

        [BsonElement("action")]
        public string Action { get; set; }

        [BsonElement("location")]
        public CarHistoryLocation Location { get; set; }

        [BsonElement("coordinates")]
        public List<double> Coordinates { get; set; }

        [BsonElement("fuel")]
        public CarHistoryFuel Fuel { get; set; }

        [BsonElement("station")]
        public CarHistoryStation Station { get; set; }
    }
}
