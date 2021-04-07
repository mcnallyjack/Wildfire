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
    public partial class ChangePass : ContentPage
    { 
        IAuth auth;
        public ChangePass()
        {
            InitializeComponent();
        }

        private async void ChangePassword_Clicked(object sender, EventArgs e)
        {
            if (oldPass.Text.Length < 8)
            {
                await DisplayAlert("Error", "Password length must be greater than 8", "ok");
            }
            else
            {
                var authService = DependencyService.Resolve<IAuth>();
                await authService.ChangePassword(oldPass.Text);
                await DisplayAlert("Success", "Password Changed", "ok");
                await Navigation.PushModalAsync(new MainTabPage());
            }
            }
        }
    }
