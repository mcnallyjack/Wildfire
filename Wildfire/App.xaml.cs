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
            Device.SetFlags(new string[] {"Brush_Experimental"});
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            MainPage = new MainTabPage();

            if (auth.SignIn())
            {
                MainPage = new SplashScreen();
            }
            else
            {
                MainPage = new SplashScreen();
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
