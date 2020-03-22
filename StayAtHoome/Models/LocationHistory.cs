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
        
        public LocationHistory(DateTimeOffset @from, Entry[] entries)
        {
            From = from;
            Entries = entries;
            Today = entries.FirstOrDefault(x => x.Day.Date == DateTimeOffset.Now.Date);
        }

        public DateTimeOffset From { get; }
        public Entry[] Entries { get; }

        public Entry Today { get; }
    }
}