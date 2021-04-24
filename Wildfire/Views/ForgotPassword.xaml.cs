/* Author:      Jack McNally
 * Page Name:   ForgotPassword
 * Purpose:     Backend for Forgot Password.
 */
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

        //Forgot Password Event Handler
        private async void ForgotPass_Clicked(object sender, EventArgs e)
        {
            try
            {
                var authService = DependencyService.Resolve<IAuth>();
                await authService.ForgotPassword(emailPass.Text);
                await DisplayAlert("Success", "Please check email inbox", "ok");
                await Navigation.PushModalAsync(new LoginPageView());
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            
        }

        //Back Button Event Handler
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new LoginPageView());
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            
        }
    }
}