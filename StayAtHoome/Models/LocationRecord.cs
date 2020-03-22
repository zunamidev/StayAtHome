using System;
using SQLite;
using Xamarin.Essentials;

namespace StayAtHoome.Models
{
    public class LocationRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Accuracy { get; set; }
        
        public DateTimeOffset Timestamp { get; set; }
        
        public Location Location => new Location(Latitude, Longitude, Timestamp);
    }
}