﻿using System;
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
    public partial class ResolveFireInfoView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ResolveFireInfoView(string Label, string placeName, string tag)
        {
            InitializeComponent();
            fireID.Text = $"{placeName}";
            firePlaceName.Text = $"{Label}";
            fireTag.Text = $"{tag}";
        }

        private async void btn_Res_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.ResolveFire(Convert.ToString(fireTag.Text));
            await DisplayAlert("Success", "Fire Resolved", "OK");
            await Navigation.PushModalAsync(new MainTabPage());
        }
    }
}