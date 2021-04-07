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
    public partial class SignUpPageView : ContentPage
    {
        IAuth auth;
        public SignUpPageView()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
        }

        async void SignUp_Clicked(object sender, EventArgs e)
        {

            if (PasswordInput.Text.Length < 8)
            {
                await DisplayAlert("Error", "Password must be greater than 8 characters", "ok");
            }
            else if (firefighterCon.IsChecked != true)
            {
                await DisplayAlert("Error", "Please verify is you are a firefighter", "ok");
            }
            else {


                var user = auth.SignUpWithEmailAndPassword(EmailInput.Text, PasswordInput.Text);
                if (user != null)
                {
                    await DisplayAlert("Success", "Created", "Ok");
                    var signOut = auth.SignOut();

                    if (signOut)
                    {
                        Application.Current.MainPage = new LoginPageView();
                    }
                    else
                    {
                        await DisplayAlert("Error", "unable to LogOut", "Ok");
                    }
                }
            
            }
        }
        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FirstPageView());
        }
    }
}