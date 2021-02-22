using KeepTrack.Domain.Models;

namespace KeepTrack.InventoryComponent.Domain.Models
{
    public class BookModel : IDataModel
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string Title { get; set; }
    }
}
