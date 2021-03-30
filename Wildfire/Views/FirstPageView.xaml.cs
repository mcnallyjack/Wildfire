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
    public partial class FirstPageView : ContentPage
    {
        IAuth auth;
        public FirstPageView()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        private void CreateAcc_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            try
            {
                if (signOut)
                    {
                        Application.Current.MainPage = new SignUpPageView();
                    } 
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void LoginIn_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            try
            {
                if (signOut)
                    {
                        Application.Current.MainPage = new LoginPageView();
                    }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private async void Continue_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new MainTabPage());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        
    }
}