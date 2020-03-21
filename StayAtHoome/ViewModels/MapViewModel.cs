using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using StayAtHoome.Models;
using StayAtHoome.Views;

namespace StayAtHoome.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public Xamarin.Forms.Maps.Map map = new Xamarin.Forms.Maps.Map();
        public MapViewModel()
        {
            Title = "Map";            
        }

        public void setMap(Xamarin.Forms.Maps.Map _map)
        {
            map = _map;
        }

        //async Task LoadSthCommand()
        //{
            
        //}
    }
}