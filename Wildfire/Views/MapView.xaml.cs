using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wildfire.Views;
using Xamarin.Essentials;


namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();
            Pin pinCarlow = new Pin()
            {
                Type = PinType.Place,
                Label = "Carlow",
                Address = "Carlow, Ireland",
                Position = new Position(52.8365d, -6.9341d),

            };
            map.Pins.Add(pinCarlow);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pinCarlow.Position, Distance.FromMeters(5000)));
        }

       

        private async void Search_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new SearchView() { BindingContext = this.BindingContext }, false);

        }

        void RemovePopupTapped(object sender, EventArgs e)
        {
            popupSearch.IsVisible = false;
        }

        private async void map_MapClicked(object sender, MapClickedEventArgs e)
        {
            Pin newFire = new Pin()
            {
                Label = "New Fire",
                Position = new Position(e.Point.Latitude, e.Point.Longitude),
                IsDraggable = true
                
            };
            map.Pins.Add(newFire);
            await Task.Delay(2000);
            await Navigation.PushModalAsync(new ReportFireInfoView() { BindingContext = this.BindingContext }, false);
        }
    }
}