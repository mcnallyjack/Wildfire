using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoView : ContentPage
    {
        
        public InfoView()
        {
            InitializeComponent();
            
        }
        private async void LocalInfo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LocalInfoView());
        }

        private async void ProfessionalInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(LoginPageView.token != null)
                {
                    await Navigation.PushModalAsync(new ProFireInfoView());
                }
                else
                {
                    await DisplayAlert("Error", "Only Firefighters can access this Page", "ok");
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private async void HomeInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new HomeInfoView());
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private async void WildlandInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new WildlandInfoView());
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}