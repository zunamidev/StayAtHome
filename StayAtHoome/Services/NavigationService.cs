using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StayAtHoome.Services
{
    public class NavigationService
    {
        public async Task PopModal()
        {
            Debug.WriteLine(Application.Current.MainPage.Navigation.ModalStack);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}