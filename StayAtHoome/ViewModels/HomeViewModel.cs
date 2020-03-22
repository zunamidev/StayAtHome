using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Microcharts;
using MoreLinq;
using SkiaSharp;
using StayAtHoome.Data;
using StayAtHoome.Models;
using StayAtHoome.Services;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

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

    public class HomeViewModel : BaseViewModel
    {
        public User User { get; private set; }

        public string Greeting => User != null ? $"Hallo {User.Name}" : "";

        public LocationHistory LocationHistory { get; private set; }

        public WeeksViewModel[] WeeksViewModels { get; set; } = { };

        public HomeViewModel()
        {
            var userRepo = DependencyService.Get<UserRepository>();
            userRepo.UserChanged += () => ReloadData().SafeFireAndForget();
            ReloadData().SafeFireAndForget();
        }

        private async Task ReloadData()
        {
            await UpdateUser();
            LocationHistory = await DependencyService.Get<LocationHistoryService>()
                .GetLocationHistory(DateTimeOffset.Now.AddMonths(-1));
            OnPropertyChanged(nameof(LocationHistory));

            UpdateWeeksViewModels();
        }

        private void UpdateWeeksViewModels()
        {
            var calendar = CultureInfo.CurrentCulture.Calendar;
            WeeksViewModels = LocationHistory.Entries
                .GroupBy(x => calendar.GetWeekOfYear(x.Day.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                .Select((entries, weekNumber) =>
                {
                    var day = entries.First().Day;
                    return new WeeksViewModel(StartOfWeek(day), entries.ToArray());
                }).ToArray();
            OnPropertyChanged(nameof(WeeksViewModels));
        }

        private DateTimeOffset StartOfWeek(DateTimeOffset day)
        {
            var start = day.AddDays(-(int) DateTime.Today.DayOfWeek + (int) DayOfWeek.Monday);
            if (start > day)
            {
                start = start.AddDays(-7);
            }

            return start;
        }

        private async Task UpdateUser()
        {
            var userRepo = DependencyService.Get<UserRepository>();
            User = await userRepo.GetUserAsync();
            OnPropertyChanged(nameof(Greeting));
        }
    }
}