/* Author:      Jack McNally
 * Page Name:   InfoView
 * Purpose:     Backend for Information View.
 */
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

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        // Local Info Button Handler
        private async void LocalInfo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LocalInfoView());
        }

        // Firefighter Info Button Handler
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

        // Home Info Button Handler
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

        // Wildland Info Button Handler
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