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
                UserMap = new Map();
                if (location != null)
                {
                    var centerPosition = new Position(location.Latitude, location.Longitude);
                    UserMap.MoveToRegion(MapSpan.FromCenterAndRadius(centerPosition, Distance.FromKilometers(10)));

                    UserMap.Pins.Add(new Pin
                    {
                        Position = centerPosition,
                        Label = "Ich"
                    });
                }

                var homeLocation = user?.HomeLocation;

                if (homeLocation != null)
                {
                    var homePosition = new Position(homeLocation.Latitude, homeLocation.Longitude);
                    UserMap.Pins.Add(new Pin
                    {
                        Position = homePosition,
                        Label = "Zuhause"
                    });
                    if (location == null)
                    {
                        UserMap.MoveToRegion(MapSpan.FromCenterAndRadius(homePosition, Distance.FromKilometers(10)));
                    }
                }
                
                OnPropertyChanged(nameof(UserMap));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

        }
    }
}
