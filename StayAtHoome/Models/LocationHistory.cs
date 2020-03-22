using System;
using System.Linq;

namespace StayAtHoome.Models
{
    public class LocationHistory
    {
        public class Entry
        {
            public DateTimeOffset Day { get; set; }
            public int HoursAtHome { get; set; }
            public int HoursTotal { get; set; } = 24;
            public int MissingDataHours { get; set; }

            public int HoursNotAtHome => HoursTotal - HoursAtHome - MissingDataHours;

            public DayOfWeek DayOfWeek => Day.DayOfWeek;
        }
        
        public LocationHistory(DateTimeOffset @from)
        {
            From = from;
        }

        public DateTimeOffset From { get; set; }
        public Entry[] Entries { get; set; } = { };

        public Entry Today => Entries?.FirstOrDefault(x => x.Day.Date == DateTimeOffset.Now.Date);
    }
}