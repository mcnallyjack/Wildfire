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
        public ResolveFireInfoView()
        {
            InitializeComponent();
        }

        private async void btn_Res_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.ResolveFire(Convert.ToInt32(fireID.Text));
            await DisplayAlert("Success", "Fire Resolved", "OK");
            await Navigation.PopModalAsync();
        }
    }
}