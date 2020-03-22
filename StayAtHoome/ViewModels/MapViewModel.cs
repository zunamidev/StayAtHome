using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using StayAtHoome.Data;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Map = Xamarin.Forms.Maps.Map;

namespace StayAtHoome.ViewModels
{
    class MapViewModel : BaseViewModel
    {
        public Map UserMap
        {
            get;
            set;
        }

        public MapViewModel()
        {
            UpdateLocation().SafeFireAndForget();
        }

        private async Task UpdateLocation()
        {
            try
            {
                var user = await DependencyService.Get<UserRepository>().GetUserAsync();
                var location = await Geolocation.GetLocationAsync();
                UserMap.Pins.Clear();
                if (location != null)
                {
                    var centerPosition = new Position(location.Latitude, location.Longitude);
                    UserMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(10)));

                    var current = new Pin
                    {
                        Position = centerPosition,
                        Label = "Ich"
                    };
                    UserMap.Pins.Add(current);
                }
            }
            catch (Exception e)
            {
                
            }

        }
    }
}
