using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StayAtHoome.Models;
using StayAtHoome.ViewModels;
using Xamarin.Forms.Maps;

namespace StayAtHoome.Views
{
    [DesignTimeVisible(false)]
//    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Map : ContentPage
    {
        MapViewModel viewModel;

        public Map()
        {
            InitializeComponent();

            BindingContext = viewModel = new MapViewModel();

            var map = new Xamarin.Forms.Maps.Map(MapSpan.FromCenterAndRadius(
                new Position(49.0068901, 8.4036527),
                Distance.FromKilometers(10)));

            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(48.978823, 8.333481),
                Label = "Messe Karlsruhe"
            };

            var pin2 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(49.000338, 8.472183),
                Label = "Karlsruhe-Durlach"
            };

            map.IsShowingUser = true;
            map.IsEnabled = true;
            map.ScaleX = 100.0;
            map.ScaleY = 100.0;
            map.MapClicked += MapClicked;

            viewModel.setMap(map);
            BindingContext = viewModel;
        }

        private void MapClicked(object sender, MapClickedEventArgs e)
        {
            throw new Exception();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel == null)
                throw new Exception();
        }
    }
}




//        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
//        {
//            var item = args.SelectedItem as Item;
//            if (item == null)
//                return;

//            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

//            // Manually deselect item.
//            ItemsListView.SelectedItem = null;
//        }

//        async void AddItem_Clicked(object sender, EventArgs e)
//        {
//            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
//        }

//        protected override void OnAppearing()
//        {
//            base.OnAppearing();

//            if (viewModel.Items.Count == 0)
//                viewModel.LoadItemsCommand.Execute(null);
//        }
//    }
//}