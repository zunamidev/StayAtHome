using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;
namespace StayAtHoome.ViewModels
{
    class MapViewModel : BaseViewModel
    {
        public Position userLocation
        {
            get;
            set;
        }
        public Position homeLocation
        {
            get;
            set;
        }

        public Int32 distance
        {
            get;
            set;
        }

        public Map userMap
        {
            get;
            set;
        }

        public MapViewModel()
        {
            userLocation = new Position();
            homeLocation = new Position();

            userMap = new Map(MapSpan.FromCenterAndRadius(
                new Position(49.007038, 8.397768), Distance.FromKilometers(10)));

            Pin p1 = new Pin();
            p1.Position = new Position(49.008851, 8.398506);
            p1.Label = "Alte Bank - Gutes Restaurant, excellente Paella gegessen";

            userMap.Pins.Add(p1);
        }
    }
}
