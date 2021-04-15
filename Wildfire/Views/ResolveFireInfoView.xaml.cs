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
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResolveFireInfoView : ContentPage
    {
        MediaFile file;
        MediaFile mediaFile;
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        
        public ResolveFireInfoView(string Label, string placeName, string tag)
        {
            InitializeComponent();
            fireID.Text = $"{placeName}";
            firePlaceName.Text = $"{Label}";
            fireTag.Text = $"{tag}";
            time.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            var filename = fireTag.Text;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            overlay.IsVisible = true;
            imgChoose.IsVisible = false;
            await LoadImage();
            imgChoose.IsVisible = true;
            overlay.IsVisible = false;         
        }

        public async Task/*<ImageSource>*/ LoadImage()
        {
            try
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
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            
        }

        private async void btn_Res_Clicked(object sender, EventArgs e)
        {
            
            await firebaseHelper.ResolveFire(Convert.ToString(fireTag.Text));
            await DisplayAlert("Success", "Fire Resolved", "OK");
            await firebaseHelper.AddResolvedFire(fireTag.Text, firePlaceName.Text, fireID.Text, newDesc.Text,time.Text);
            await Navigation.PushModalAsync(new MainTabPage());
        }

        private async void btn_Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainTabPage());
        }

        private async void photo_Add_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgNewChoose.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = file.GetStream();
                    return imageStram;
                });
                await StoreImages(file.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            await StoreImages(file.GetStream());
        }

        public async Task<string> StoreImages(Stream imageStream)
        {
            var fileName = fireTag.Text;
            var stroageImage = await new FirebaseStorage("driven-bulwark-297919.appspot.com")
                .Child("Fires")
                .Child(fileName + "(new)" + ".jpeg")

                .PutAsync(imageStream);
            string imgurl = stroageImage;
            return imgurl;
        }

        private async void photo_Take_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                mediaFile = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    AllowCropping = true
                });

                // var result = await MediaPicker.CapturePhotoAsync();

                // var imageStream = await result.OpenReadAsync();

                imgNewChoose.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = mediaFile.GetStream();
                    return imageStram;
                });
                await StoreImages(mediaFile.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}