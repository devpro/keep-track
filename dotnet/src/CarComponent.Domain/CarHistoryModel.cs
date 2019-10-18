using System;

namespace KeepTrack.CarComponent.Domain
{
    public class CarHistoryModel
    {
        public string Id { get; set; }
        public string CarId { get; set; }
        public DateTime HistoryDate { get; set; }
    }
}
