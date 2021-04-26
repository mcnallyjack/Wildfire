/* Author:      Jack McNally
 * Page Name:   LoginPageView
 * Purpose:     Backend for Login Page.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wildfire.Services;
using Xamarin.Essentials;
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
            MapView.notificationCount = 0;
            MapView.loginCount = 0;
        }

        // Login Button Handler
        private async void Login_Clicked(object sender, EventArgs e)
        {
            if(EmailInput.Text == null)
            { 
                await DisplayAlert("Error", "Please enter email", "Ok");
                return;
            }
            if (PasswordInput.Text == null)
            {
                await DisplayAlert("Error", "Please enter password", "Ok");
                return;
            }
            token = await auth.LoginWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);

            try
            { 
               
                if (token != string.Empty)
                {
                    MapView.loginCount = 0;
                    await Navigation.PushModalAsync(new MainTabPage());
                }
                else
                {
                    await DisplayAlert("Auth Failed", "Email & Password Incorrect", "Ok");
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // Sign Up Button Handler
        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            try
            {
                if (signOut)
                {
                    await Navigation.PushModalAsync(new SignUpPageView());
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // Back Button Handler
        private async void Back_Clicked(object sender, EventArgs e)
        {
            try
            {
                MapView.loginCount = 0;
                await Navigation.PushModalAsync(new FirstPageView());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // Forgot Button Handler
        private async void Forgot_Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new ForgotPassword());
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }            
        }
    }
}