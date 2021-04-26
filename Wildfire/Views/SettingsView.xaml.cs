/* Author:      Jack McNally
 * Page Name:   SettingsView
 * Purpose:     Backend for Settings View.
 */
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
            radius = radiusSettings.Text;
            if (radius == "")
            {
                radius =null;
            }
            notificationSettings.IsChecked = Preferences.Get("notificationSettings", false);

            OnPropertyChanged();

            auth = DependencyService.Get<IAuth>();
            settingsDay.Text = DateTime.Now.DayOfWeek.ToString();
            settingsDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        // Login Button Event Handler
        private async void Login_Button_Clicked(object sender, EventArgs e)
        {
            if (LoginPageView.token == null)
            {
                MapView.notificationCount = 1;
                await Navigation.PushModalAsync(new LoginPageView());
            }
            else
            {
                await DisplayAlert("Already Logged in", "", "Yes");
            }
        }

        // SignUp Button Event Handler
        private async void Signup_Button_Clicked(object sender, EventArgs e)
        {
            if (LoginPageView.token != null)
            {
                await DisplayAlert("You already have an account", "", "Yes");
            }
            else
            {
                await Navigation.PushModalAsync(new SignUpPageView());
            }
        }

        // Logout Button Event Handler
        private async  void Logout_Button_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            if (LoginPageView.token != null)
            {
                if (signOut)
                {
                    MapView.notificationCount = 0;
                    LoginPageView.token = null;
                    MapView.fireNotCount = 0;
                    MapView.loginCount = 0;
                    await Navigation.PushModalAsync(new FirstPageView());
                }
            }
            else
            {
               await  DisplayAlert("Not Logged in", "", "Yes");
            }
        }

        // OnChange Event Handler
        private void OnChange(object sender, EventArgs e)
        {
            Preferences.Set("radiusSettings", radiusSettings.Text);
            Preferences.Set("notificationSettings", notificationSettings.IsChecked);
            
            if (radiusSettings.Text == string.Empty)
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
        }

        // OnDisapperaing Event Handler
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // Change Password Button Event Handler
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