using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalInfoView : ContentPage
    {
        public LocalInfoView()
        {
            InitializeComponent();
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ActivityIndicator activity = new ActivityIndicator() { IsRunning = true };
            await LoadLocation();
            activity.IsRunning = false;
        }

        async Task LoadLocation()
        {
           
            var location = await Geolocation.GetLocationAsync();
            if (location != null)
            {
                var plLat = location.Latitude;
                var plLong = location.Longitude;
                var placemark1 = await Geocoding.GetPlacemarksAsync(plLat, plLong);
                var placemarkDetails1 = placemark1?.FirstOrDefault();
                string locality1 = placemarkDetails1.AdminArea;
                string areaCode1 = placemarkDetails1.CountryCode;
                string Place1 = locality1 + ", " + areaCode1;

                InfoLocation.Text = Place1.ToString();
            }
        }
    }
}