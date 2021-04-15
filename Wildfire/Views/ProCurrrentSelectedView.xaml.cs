using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wildfire.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProCurrrentSelectedView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ProCurrrentSelectedView(string WindDirection, string PlaceName, string FireID, string Time, string Description)
        {
            InitializeComponent();
            
            placeName.Text = PlaceName;
            description.Text = Description;
            time.Text = Time;
            windDir.Text = WindDirection;
            fireID.Text = FireID;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            overlay.IsVisible = true;
            currentFire.IsVisible = false;
            await LoadImage();
            currentFire.IsVisible = true;
            overlay.IsVisible = false;
        }

        public async Task LoadImage()
        {
            try
            {
                var filename = fireID.Text;
                var webClient = new WebClient();
                var storageImage = await new FirebaseStorage("driven-bulwark-297919.appspot.com")
                    .Child("Fires")
                    .Child(filename + ".jpeg")
                    .GetDownloadUrlAsync();
                string imgurl = storageImage;
                byte[] imgbytes = webClient.DownloadData(imgurl);
                currentFire.Source = ImageSource.FromStream(() => new MemoryStream(imgbytes));
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}