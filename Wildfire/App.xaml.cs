using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wildfire.Services;
using Wildfire.Views;

namespace Wildfire
{
    public partial class App : Application
    {
        IAuth auth;
        public App()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            MainPage = new MainTabPage();

            if (auth.SignIn())
            {
                MainPage = new FirstPageView();
            }
            else
            {
                MainPage = new FirstPageView();
            }
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
