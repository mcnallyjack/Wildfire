
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
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.IO;


namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportFireInfoView : ContentPage
    {
        MediaFile file;
        MediaFile MediaFile;
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
            deviceID.Text = Android.OS.Build.GetSerial().ToString();
            
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
            await firebaseHelper.AddFire(Convert.ToString(Id), myLat.Text, myLong.Text, timeFound.Text, directionEntry.Items[directionEntry.SelectedIndex], FireDesc.Text, placeName.Text, deviceID.Text);
            fireID.Text = string.Empty;
            myLat.Text = string.Empty;
            myLong.Text = string.Empty;
            timeFound.Text = string.Empty;
            directionEntry.Items[directionEntry.SelectedIndex] = string.Empty;
            FireDesc.Text = string.Empty;
            placeName.Text = string.Empty;
            deviceID.Text = string.Empty;
            await DisplayAlert("Success", "Added", "OK");
            var allFires = await firebaseHelper.GetAllFires();
            await Navigation.PushModalAsync(new MainTabPage());
            
        }

        private async void btn_Cancel_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void directionEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var name = directionEntry.Items[directionEntry.SelectedIndex];
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
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
                imgChoose.Source = ImageSource.FromStream(() =>
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

        private async void photo_Take_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                MediaFile = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    AllowCropping = true
                });
                
               // var result = await MediaPicker.CapturePhotoAsync();

               // var imageStream = await result.OpenReadAsync();

                imgChoose.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = MediaFile.GetStream();
                    return imageStram;
                });
                await StoreImages(MediaFile.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<string> StoreImages(Stream imageStream)
        {
            var fileName = Id;
            var stroageImage = await new FirebaseStorage("driven-bulwark-297919.appspot.com")
                .Child("Fires")
                .Child(fileName + ".jpeg")
                
                .PutAsync(imageStream);
            string imgurl = stroageImage;
            return imgurl;
        }
    }
}