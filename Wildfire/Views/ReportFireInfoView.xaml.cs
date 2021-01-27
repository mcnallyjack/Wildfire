
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

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportFireInfoView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ReportFireInfoView()
        {
            InitializeComponent();
        }

        private async void btn_Add_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddFire(Convert.ToInt32(fireID.Text));
            fireID.Text = string.Empty;
            await DisplayAlert("Success", "Added", "OK");
            var allFires = await firebaseHelper.GetAllFires();
            
        }
    }
}