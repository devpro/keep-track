namespace KeepTrack.InventoryComponent.Domain.Models
{
    public interface IDataModel
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }
    }
}
