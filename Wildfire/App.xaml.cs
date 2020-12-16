using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wildfire.Services;

namespace Wildfire
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            MainPage = new MainTabPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
