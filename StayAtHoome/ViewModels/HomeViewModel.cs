using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using StayAtHoome.Data;
using StayAtHoome.Models;
using StayAtHoome.Services;
using Xamarin.Forms;

namespace StayAtHoome.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public User User { get; private set; }

        public string Greeting => User != null ? $"Hallo {User.Name}" : "";

        public LocationHistory LocationHistory { get; private set; }

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
        }

        private async Task UpdateUser()
        {
            var userRepo = DependencyService.Get<UserRepository>();
            User = await userRepo.GetUserAsync();
            OnPropertyChanged(nameof(Greeting));
        }
    }
}