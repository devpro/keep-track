using MongoDB.Bson;

namespace KeepTrack.Dal.MongoDb.Entities
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
        string OwnerId { get; set; }
    }
}
