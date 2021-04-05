using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]



    public class Dispatch
    {
        public List<string> all { get; set; }
        public object gsm { get; set; }
        public object @fixed { get; set; }
    }



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

                if (areaCode1 == "IE")
                {
                    var num = "999 or 112";
                    EmerNum.Text = num;
                }
                else if (areaCode1 == "GB")
                {
                    var num = "999";
                    EmerNum.Text = num;

                }
                else
                {
                    var num = "Doesn't Exist";
                    EmerNum.Text = num;
                }
            }


        }
    }
}


        
    

  
