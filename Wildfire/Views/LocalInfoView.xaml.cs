/* Author:      Jack McNally
 * Page Name:   LocalInfoView
 * Purpose:     Backend for Local Info.
 */
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

    public partial class LocalInfoView : ContentPage
    {
        public LocalInfoView()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Loading Logic
        /// </summary>
        protected async override void OnAppearing()
        {

            base.OnAppearing();

            overlay.IsVisible = true;
            loading.IsVisible = true;
            FrameLabel.IsVisible = false;
            TopLabel.IsVisible = false;
            InfoLocation.IsVisible = false;
            EmerLabel.IsVisible = false;
            EmerNum.IsVisible = false;
            DialerButton.IsVisible = false;
            BackButton.IsVisible = false;
            await LoadLocation();
            overlay.IsVisible = false;
            loading.IsVisible = false;
            FrameLabel.IsVisible = true;
            TopLabel.IsVisible = true;
            InfoLocation.IsVisible = true; 
            EmerLabel.IsVisible = true;
            EmerNum.IsVisible = true;
            DialerButton.IsVisible = true;
            BackButton.IsVisible = true;
        }

        /// <summary>
        /// Load the current location of the user
        /// </summary>
        /// <returns></returns>
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
                    var num = "999";
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

        // Back Button Handler
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }     
        }

        // Dialer Button Handler
        private void DialerButton_Clicked(object sender, EventArgs e)
        {
            var emergencyNum = EmerNum.Text;
            if (emergencyNum != null)
            {
                try
                {
                    PhoneDialer.Open(emergencyNum);
                }
                catch (ArgumentNullException anEx)
                {
                    anEx.Message.ToString();
                }
                catch (FeatureNotSupportedException ex)
                {
                    ex.Message.ToString();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            else
            {
                DisplayAlert("Error", "No number", "ok");
            }
        }
    }
}


        
    

  
