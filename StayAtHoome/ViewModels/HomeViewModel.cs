using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using StayAtHoome.Data;
using StayAtHoome.Models;
using Xamarin.Forms;

namespace StayAtHoome.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public User User
        {
            get;
            private set;
        }

        public string Greeting => User != null ? $"Hallo {User.Name}" : "";

        public HomeViewModel()
        {
            var userRepo = DependencyService.Get<UserRepository>();
            userRepo.UserChanged += () => { UpdateUser().SafeFireAndForget(); };
            UpdateUser().SafeFireAndForget();
        }

        private async Task UpdateUser()
        {
            var userRepo = DependencyService.Get<UserRepository>();
            User = await userRepo.GetUserAsync();
            OnPropertyChanged(nameof(Greeting));
        }
    }
}