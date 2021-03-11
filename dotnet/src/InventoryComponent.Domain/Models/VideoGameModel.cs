using KeepTrack.Domain.Models;

namespace KeepTrack.InventoryComponent.Domain.Models
{
    public class VideoGameModel : IDataModel
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string Title { get; set; }

        public string Platform { get; set; }

        public int Year { get; set; }
    }
}
