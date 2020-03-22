using System;
using System.ComponentModel;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using StayAtHoome.Background;
using StayAtHoome.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StayAtHoome.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            CheckUserExists().SafeFireAndForget();
            new PeriodicLocationTracker().Execute().SafeFireAndForget();
        }

        private async Task CheckUserExists()
        {
            await LocalDatabase.WaitInitialized;
            var userRepo = DependencyService.Get<UserRepository>();
            var user = await userRepo.GetUserAsync();
            if (user != null)
                return;

            await Navigation.PushModalAsync(new OnboardingPage());
        }
    }
}