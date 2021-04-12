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
    public partial class ProResSelectedView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ProResSelectedView(string PlaceName, string FireID, string Time, string ResDescription)
        {
            InitializeComponent();
            placeName.Text = PlaceName;
            time.Text = Time;
            fireID.Text = FireID;
           
            description.Text = ResDescription;

            currentFire.Source = LoadImage().ToString();
        }

        public async Task<ImageSource> LoadImage()
        {
            var filename = fireID.Text;

            var webClient = new WebClient();
            var storageImage = await new FirebaseStorage("driven-bulwark-297919.appspot.com")
                .Child("Fires")
                .Child(filename + "(new)" + ".jpeg")
                .GetDownloadUrlAsync();
            string imgurl = storageImage;
            byte[] imgbytes = webClient.DownloadData(imgurl);
            currentFire.Source = ImageSource.FromStream(() => new MemoryStream(imgbytes));
            return currentFire.Source;

        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}