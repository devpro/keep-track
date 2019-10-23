﻿using System;

namespace KeepTrack.CarComponent.Domain
{
    public class CarHistoryModel
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string CarId { get; set; }

        public DateTime HistoryDate { get; set; }

        public int Mileage { get; set; }

        public string Action { get; set; }

        public string City { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public string FuelCategory { get; set; }

        public double? FuelVolume { get; set; }

        public double? FuelUnitPrice { get; set; }

        public double? Amount { get; set; }

        public bool? IsFullTank { get; set; }

        public double? DeltaMileage { get; set; }

        public string LastRefuelHistoryId { get; set; }

        public string StationBrandName { get; set; }
    }
}
