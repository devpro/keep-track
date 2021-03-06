﻿using KeepTrack.Domain.Models;

namespace KeepTrack.InventoryComponent.Domain.Models
{
    public class TvShowModel : IDataModel
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string Title { get; set; }
    }
}
