using MongoDB.Bson.Serialization.Attributes;

namespace KeepTrack.CarComponent.Infrastructure.MongoDb.Entities
{
    public partial class CarHistoryLocation
    {
        [BsonElement("city")]
        public string City { get; set; }
    }
}
