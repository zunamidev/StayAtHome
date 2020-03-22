using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using GalaSoft.MvvmLight.Command;
using StayAtHoome.Data;
using StayAtHoome.Models;
using StayAtHoome.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StayAtHoome.ViewModels
{
    public class OnboardingViewModel : BaseViewModel
    {
        public RelayCommand SaveUserCommand { get; }

        private string _userName = string.Empty;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private bool _currentlyHome;

        public bool CurrentlyHome
        {
            get => _currentlyHome;
            set => SetProperty(ref _currentlyHome, value);
        }

        private bool _hasNoHomeLocation;

        public bool HasNoHomeLocation
        {
            get => _hasNoHomeLocation;
            set => SetProperty(ref _hasNoHomeLocation, value);
        }

        public OnboardingViewModel()
        {
            SaveUserCommand = new RelayCommand(() => SaveUser().SafeFireAndForget(), CanSaveUser);

            PropertyChanged += (sender, args) => SaveUserCommand.RaiseCanExecuteChanged();
            LoadUser().SafeFireAndForget();
        }

        private async Task LoadUser()
        {
            var user = await DependencyService.Get<UserRepository>().GetUserAsync();
            if (user == null) return;
            UserName = user.Name;
            HasNoHomeLocation = user.HomeLatitude == null;
        }

        private bool CanSaveUser()
        {
            return !string.IsNullOrWhiteSpace(_userName);
        }

        private async Task SaveUser()
        {
            var userRepo = DependencyService.Get<UserRepository>();

            var user = await userRepo.GetUserAsync() ?? new User {Name = UserName};
            if (CurrentlyHome)
            {
                var homeLocation = await GetHomeLocation();
                if (homeLocation != null)
                {
                    user.HomeAccuracy = homeLocation.Accuracy;
                    user.HomeLatitude = homeLocation.Latitude;
                    user.HomeLongitude = homeLocation.Longitude;
                }
            }

            await userRepo.UpdateUserAsync(user);

            var navService = DependencyService.Get<NavigationService>();
            await navService.PopModal();
        }

        private static async Task<Location> GetHomeLocation()
        {
            try
            {
                return await Geolocation.GetLocationAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error getting home location: {e}");
                return null;
            }
        }
    }
}