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
    public partial class LoginPageView : ContentPage
    {
        IAuth auth;
        public static string token;
        public LoginPageView()
        {

            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            token = await auth.LoginWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
            if(token != string.Empty)
            {
                await Navigation.PushModalAsync(new MainTabPage());
            }
            else
            {
                await DisplayAlert("Auth Failed", "Email & Password Incorrect", "Ok");
            }
        }

        private  void SignUp_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            if (signOut)
            {
                Application.Current.MainPage = new SignUpPageView();
            }
        }
    }
}