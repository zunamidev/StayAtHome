using SQLite;
using Xamarin.Essentials;

namespace StayAtHoome.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public double? HomeLatitude { get; set; }
        public double? HomeLongitude { get; set; }
        public double? HomeAccuracy { get; set; }

        public bool HasHomeLocation => HomeLatitude != null;
        
        public Location HomeLocation => HomeLatitude != null && HomeLongitude != null ? new Location(HomeLatitude.Value, HomeLongitude.Value) : null;
    }
}