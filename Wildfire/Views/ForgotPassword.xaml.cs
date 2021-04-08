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
    public partial class ForgotPassword : ContentPage
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private async void ForgotPass_Clicked(object sender, EventArgs e)
        {
            var authService = DependencyService.Resolve<IAuth>();
            await authService.ForgotPassword(emailPass.Text);
            await DisplayAlert("Success", "Please check email inbox", "ok");
            await Navigation.PushModalAsync(new LoginPageView());
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPageView());
        }
    }
}