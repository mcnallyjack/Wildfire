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
        public SettingsView()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            settingsDay.Text = DateTime.Now.DayOfWeek.ToString();
            settingsDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        private void Logout_Button_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            if (LoginPageView.token != null)
            {

                if (signOut)
                {
                    Application.Current.MainPage = new FirstPageView();
                }
            }
            else
            {
                DisplayAlert("Not Logged in", "", "Yes");
            }
        }
    }
}