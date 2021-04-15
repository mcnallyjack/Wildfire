using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wildfire.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wildfire.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        IAuth auth;
        public static string radius;
        public static bool isChecked;
        public SettingsView()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("Radius"))
            {
                radiusSettings.Text = Application.Current.Properties["Radius"].ToString();
            }
            if (Application.Current.Properties.ContainsKey("NotificationEnabled"))
            {
                notificationSettings.IsChecked = (bool)Application.Current.Properties["NotificationEnabled"];
            }
            auth = DependencyService.Get<IAuth>();
            settingsDay.Text = DateTime.Now.DayOfWeek.ToString();
            settingsDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        private void Login_Button_Clicked(object sender, EventArgs e)
        {
            if (LoginPageView.token == null)
            {
                MapView.notificationCount = 0;
                Application.Current.MainPage = new LoginPageView();
            }
            else
            {
                DisplayAlert("Already Logged in", "", "Yes");
            }
        }

        private void Signup_Button_Clicked(object sender, EventArgs e)
        {
            if (LoginPageView.token != null)
            {
                DisplayAlert("You already have an account", "", "Yes");
            }
            else
            {
                Application.Current.MainPage = new SignUpPageView();
            }
        }

        private void Logout_Button_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            if (LoginPageView.token != null)
            {

                if (signOut)
                {
                    MapView.notificationCount = 0;
                    LoginPageView.token = null;
                    MapView.fireNotCount = 0;
                    Application.Current.MainPage = new FirstPageView();
                }
            }
            else
            {
                DisplayAlert("Not Logged in", "", "Yes");
            }
        }

        private void OnChange(object sender, EventArgs e)
        {
           
            Application.Current.Properties["Radius"] = radiusSettings.Text;
            Application.Current.Properties["NotificationEnabled"] = notificationSettings.IsChecked;
            if(radiusSettings.Text == string.Empty)
            {
                radiusSettings.Text = null;
            }
            else if (radiusSettings.Text == "0")
            {
                radiusSettings.Text = null;
            }
            radius = radiusSettings.Text;
            isChecked = notificationSettings.IsChecked;
            MapView.locationCount = 0;
            MapView.notificationCount = 0;
            MapView.fireNotCount = 0;
            Application.Current.SavePropertiesAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void ChangePass_Clicked(object sender, EventArgs e)
        {
            if (LoginPageView.token != null)
            {
                await Navigation.PushModalAsync(new ChangePass());
            }
            else
            {
                await DisplayAlert("Error", "Please Login", "ok");
            }
        }
    }
}