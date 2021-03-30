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
using System.IO;
using Firebase.Storage;
using System.Net;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResolveFireInfoView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ResolveFireInfoView(string Label, string placeName, string tag)
        {
            InitializeComponent();
            fireID.Text = $"{placeName}";
            firePlaceName.Text = $"{Label}";
            fireTag.Text = $"{tag}";
            var filename = fireTag.Text;

            imgChoose.Source = LoadImage().ToString();
            Task.Delay(500);
            
        }

        public async Task<ImageSource> LoadImage()
        {
            var filename = fireTag.Text;

            var webClient = new WebClient();
            var storageImage = await new FirebaseStorage("driven-bulwark-297919.appspot.com")
                .Child("Fires")
                .Child(filename + ".jpeg")
                .GetDownloadUrlAsync();
            string imgurl = storageImage;
            byte[] imgbytes = webClient.DownloadData(imgurl);
            imgChoose.Source = ImageSource.FromStream(() => new MemoryStream(imgbytes));
            return imgChoose.Source;
            
        }

        private async void btn_Res_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.ResolveFire(Convert.ToString(fireTag.Text));
            await DisplayAlert("Success", "Fire Resolved", "OK");
            await firebaseHelper.AddResolvedFire(fireTag.Text, firePlaceName.Text, fireID.Text, newDesc.Text);
            await Navigation.PushModalAsync(new MainTabPage());
        }

        private async void btn_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainTabPage());
        }
    }
}