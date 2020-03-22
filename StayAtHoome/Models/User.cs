using SQLite;

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
    }
}