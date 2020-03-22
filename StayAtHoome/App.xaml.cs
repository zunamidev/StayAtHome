using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using StayAtHoome.Background;
using StayAtHoome.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StayAtHoome.Services;
using StayAtHoome.Views;

namespace StayAtHoome
{
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        DependencyService.Register<UserRepository>();
        DependencyService.Register<NavigationService>();
        DependencyService.Register<LocationRecordRepository>();
        MainPage = new MainPage();
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
}
}