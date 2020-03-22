using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using GalaSoft.MvvmLight.Command;
using StayAtHoome.Data;
using StayAtHoome.Services;
using Xamarin.Forms;

namespace StayAtHoome.ViewModels
{
    public class OnboardingViewModel : BaseViewModel
    {
        public RelayCommand SaveUserCommand { get; }

        private string _userName = string.Empty;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        public OnboardingViewModel()
        {
            SaveUserCommand = new RelayCommand(() => SaveUser().SafeFireAndForget(), CanSaveUser);

            PropertyChanged += (sender, args) => SaveUserCommand.RaiseCanExecuteChanged();
        }

        private bool CanSaveUser()
        {
            return !string.IsNullOrWhiteSpace(_userName);
        }

        private async Task SaveUser()
        {
            var userRepo = DependencyService.Get<UserRepository>();

            await userRepo.CreateUserAsync(_userName);

            var navService = DependencyService.Get<NavigationService>();
            await navService.PopModal();
        }
    }
}