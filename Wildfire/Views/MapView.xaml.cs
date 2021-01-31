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
            
            Task.Run(LoadCurrentPosition);
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
       
        async Task LoadCurrentPosition()
        {

            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                Pin newLoc = new Pin()
                {
                    Label = "Current Location",
                    Position = new Position(location.Latitude, location.Longitude)
                };
                map.Pins.Add(newLoc);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(2000)));
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

        private async void Location_Button_Clicked(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                Pin newLoc = new Pin()
                {
                    Label = "Current Location",
                    Position = new Position(location.Latitude, location.Longitude)
                };
                map.Pins.Add(newLoc);
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMeters(2000)));
            }
        }

        private async void ReportFire_Clicked(object sender, EventArgs e)
        {
            var location = await Geolocation.GetLocationAsync();
            if(location != null)
            {
                Pin newLoc = new Pin()
                {
                    Label = "New Fire",
                    Position = new Position(location.Latitude, location.Longitude)
                };
                map.Pins.Add(newLoc);
                var Lat = location.Latitude;
                var Long = location.Longitude;
                Lat.ToString();
                Long.ToString();
               
                await Navigation.PushModalAsync(new ReportFireInfoView(Lat, Long) { BindingContext = this.BindingContext }, false);
            }
        }

        private async void map_PinClicked(object sender, PinClickedEventArgs e)
        {
            var fires = await firebaseHelper.GetAllFires();
            
            await Navigation.PushModalAsync(new ResolveFireInfoView());
        }
    }
}