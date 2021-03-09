
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using Wildfire.Helper;
using Wildfire.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;
using Wildfire.ViewModels;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportFireInfoView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ReportFireInfoView(double Lat, double Long, string Place)
        {
            InitializeComponent();
            
            myLat.Text = $"{Lat}";
            myLong.Text = $"{Long}";
            placeName.Text = $"{Place}";
            timeFound.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            directionEntry.Items.Add("North");
            directionEntry.Items.Add("South");
            directionEntry.Items.Add("East");
            directionEntry.Items.Add("West");
            directionEntry.Items.Add("North-East");
            directionEntry.Items.Add("North-West");
            directionEntry.Items.Add("South-East");
            directionEntry.Items.Add("South-West");
            directionEntry.Items.Add("Unknown");
           




        }

      

        private async void btn_Add_Clicked(object sender, EventArgs e)
        {
            if (directionEntry.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please Select Wind Direction", "Yes");
                return;
            }
            else
            {

            }
            await firebaseHelper.AddFire(Convert.ToString(Id), myLat.Text, myLong.Text, timeFound.Text, directionEntry.Items[directionEntry.SelectedIndex], FireDesc.Text, placeName.Text);
            fireID.Text = string.Empty;
            myLat.Text = string.Empty;
            myLong.Text = string.Empty;
            timeFound.Text = string.Empty;
            directionEntry.Items[directionEntry.SelectedIndex] = string.Empty;
            FireDesc.Text = string.Empty;
            placeName.Text = string.Empty;
            await DisplayAlert("Success", "Added", "OK");
            var allFires = await firebaseHelper.GetAllFires();
            await Navigation.PushModalAsync(new MainTabPage());
            
        }

        private async void btn_Cancel_Clicked(object sender, EventArgs e)
        {
           
            await Navigation.PopModalAsync();
        }

        private void directionEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = directionEntry.Items[directionEntry.SelectedIndex];
            
        }
    }
}