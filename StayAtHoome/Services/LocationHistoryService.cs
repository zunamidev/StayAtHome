using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using StayAtHoome.Data;
using StayAtHoome.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StayAtHoome.Services
{
    public class LocationHistoryService
    {
        public static readonly double HomeRadius = 0.030;
        
        public async Task<LocationHistory> GetLocationHistory(DateTimeOffset from)
        {
            var user = await DependencyService.Get<UserRepository>().GetUserAsync();
            var history = new LocationHistory(from, new LocationHistory.Entry[]{});
            
            if (user == null || !user.HasHomeLocation)
            {
                return history;
            }
            var locations = await DependencyService.Get<LocationRecordRepository>().GetLocationRecordsAsync(from);

            var earliestRecord = locations.FirstOrDefault();
            if (earliestRecord == null) return history;

            var startDay = new DateTimeOffset(earliestRecord.Timestamp.Date, earliestRecord.Timestamp.Offset);
            var endDate = DateTimeOffset.Now;

            var currentDay = startDay;
            var entries = new List<LocationHistory.Entry>();
            while (currentDay < endDate)
            {
                var nextDay = startDay.AddDays(1);
                var entry = new LocationHistory.Entry
                {
                    Day = currentDay,
                };

                for (var i = 0; i < 24; i++)
                {
                    var currentHour = currentDay.AddHours(i);
                    var nextHour = currentDay.AddHours(i + 1);

                    var locationsInCurrentHour =
                        locations.Where(x => x.Timestamp >= currentHour && x.Timestamp < nextHour).ToArray();

                    if (locationsInCurrentHour.Length == 0)
                    {
                        entry.MissingDataHours++;
                    } else if (WasAlwaysHome(user, locationsInCurrentHour))
                    {
                        entry.HoursAtHome++;
                    }
                }

                entries.Add(entry);
                currentDay = nextDay;
            }

            return new LocationHistory(history.From, entries.ToArray());
        }

        private bool WasAlwaysHome(User user, LocationRecord[] locations)
        {
           var homeLocation = user.HomeLocation; 
           if (homeLocation == null) return false;

           return locations.All((location) =>
               Location.CalculateDistance(homeLocation, location.Location, DistanceUnits.Kilometers) < HomeRadius);
        }
    }
}