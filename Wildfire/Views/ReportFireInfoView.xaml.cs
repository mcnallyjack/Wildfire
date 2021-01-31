
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

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportFireInfoView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ReportFireInfoView(double Lat, double Long)
        {
            InitializeComponent();
            myLat.Text = $"{Lat}";
            myLong.Text = $"{Long}";
            timeFound.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            
        

            
        }

        private async void btn_Add_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddFire(Convert.ToInt32(fireID.Text), myLat.Text, myLong.Text, timeFound.Text, directionEntry.Text);
            fireID.Text = string.Empty;
            myLat.Text = string.Empty;
            myLong.Text = string.Empty;
            timeFound.Text = string.Empty;
            directionEntry.Text = string.Empty;
            await DisplayAlert("Success", "Added", "OK");
            var allFires = await firebaseHelper.GetAllFires();
            await Navigation.PopModalAsync();
            
        }

      
    }
}