﻿/* Author:      Jack McNally
 * Page Name:   ProResSelectedView
 * Purpose:     Backend for Resolved fire selected view.
 */
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
        }

        /// <summary>
        /// Loading Logic
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            overlay.IsVisible = true;
            currentFire.IsVisible = false;
            await LoadImage();
            currentFire.IsVisible = true;
            overlay.IsVisible = false;
        }
        /// <summary>
        /// Load image task from firebase
        /// </summary>
        public async Task LoadImage()
        {
            try
            {
                var filename = fireID.Text;
                var webClient = new WebClient();
                var storageImage = await new FirebaseStorage("FB_CONN_1")
                    .Child("Fires")
                    .Child(filename + "(new)" + ".jpeg")
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

        // Back Button Event Handler
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
