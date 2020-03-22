using System;
using System.Globalization;
using System.Linq;
using Microcharts;
using MoreLinq;
using SkiaSharp;
using StayAtHoome.Models;

namespace StayAtHoome.ViewModels
{
    public class WeeksViewModel
    {
        public DateTimeOffset StartDate { get; }
        public DateTimeOffset EndDate { get; }

        public LocationHistory.Entry[] Entries { get; }

        public Chart Chart { get; set; }

        public string DateString
        {
            get { return $"{StartDate.Day}.{StartDate.Month}. - {EndDate.Day}.{EndDate.Month}.{EndDate.Year}"; }
        }

        public string TotalString
        {
            get
            {
                var total = Entries.Sum(x => x.HoursAtHome);
                return $"{total} Stunden";
            }
        }

        public WeeksViewModel(DateTimeOffset startDate, LocationHistory.Entry[] entries)
        {
            StartDate = startDate;
            EndDate = startDate.AddDays(6);
            var dayNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;
            Entries = entries;
            Chart = new BarChart
            {
                MaxValue = 24,
                AnimationDuration = TimeSpan.FromMilliseconds(300),
                Entries = MoreEnumerable.Sequence(0, 6).Select(x =>
                {
                    var day = StartDate.AddDays(x);
                    var entry = entries.FirstOrDefault(e => e.Day == day) ??
                                new LocationHistory.Entry() {Day = day};

                    var dayOfWeek = (int) entry.DayOfWeek;
                    return new Entry(entry.HoursAtHome)
                    {
                        
                        ValueLabel = entry.HoursAtHome.ToString(),
                        Label = dayNames[dayOfWeek],
                        Color = SKColor.Parse("#2196F3"),
                    };
                })
            };
        }
    }
}