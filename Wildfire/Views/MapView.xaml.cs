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
using Wildfire.Helper;
using Xamarin.Forms.Internals;


namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
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

            Task.Run(LoadFires);
        }

        async Task LoadFires()
        {
            var displayFires = await firebaseHelper.GetAllFires();
            foreach(var i in displayFires)
            {
                Pin newFire = new Pin()
                {
                    Label = i.FireID.ToString(),
                    Position = new Position(Convert.ToDouble(i.Latitude), Convert.ToDouble(i.Longitude)),
                };
                map.Pins.Add(newFire);
            }

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
            var Lat = e.Point.Latitude;
            var Long = e.Point.Longitude;

            Lat.ToString();
            Long.ToString();
            await Navigation.PushModalAsync(new ReportFireInfoView(Lat, Long) { BindingContext = this.BindingContext }, false);
        }
    }
}