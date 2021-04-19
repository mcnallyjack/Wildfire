using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wildfire.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

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
 
            radiusSettings.Text = Preferences.Get("radiusSettings", string.Empty);
            radiusSettings.Completed += (sender, e) =>
            {

            };
            notificationSettings.IsChecked = Preferences.Get("notificationSettings", false);
           
            auth = DependencyService.Get<IAuth>();
            settingsDay.Text = DateTime.Now.DayOfWeek.ToString();
            settingsDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        private void RadiusSettings_Completed1(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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
            Preferences.Set("radiusSettings", radiusSettings.Text);
            radiusSettings.Completed += RadiusSettings_Completed;
            Preferences.Set("notificationSettings", notificationSettings.IsChecked);

            //Application.Current.Properties["Radius"] = radiusSettings.Text;
            //Application.Current.Properties["NotificationEnabled"] = notificationSettings.IsChecked;
            //Application.Current.SavePropertiesAsync();
            /*if (radiusSettings.SelectedIndex.ToString() == string.Empty)
            {
               // radiusSettings.SelectedIndex.ToString() == 
            }
            else if (radiusSettings.SelectedIndex.ToString == "0")
            {
                radiusSettings.SelectedIndex = null;
            }*/
            radius = radiusSettings.Text;
            isChecked = notificationSettings.IsChecked;
            MapView.locationCount = 0;
            MapView.notificationCount = 0;
            MapView.fireNotCount = 0;
            
        }

        private void RadiusSettings_Completed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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