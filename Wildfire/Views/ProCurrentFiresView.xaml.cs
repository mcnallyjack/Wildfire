/* Author:      Jack McNally
 * Page Name:   ProCurrentFiresView
 * Purpose:     Backend for Current Fires View.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wildfire.Helper;
using Wildfire.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProCurrentFiresView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ProCurrentFiresView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Loading Logic
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var fires = await firebaseHelper.GetAllFires();
            currentFires.ItemsSource = fires;
        }

        // List Item tap event handler
        private async void CurrentFires_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await Task.Delay(10);
                var fireInfo = e.Item as Fire;
                await Navigation.PushModalAsync(new ProCurrrentSelectedView(fireInfo.WindDirection, fireInfo.PlaceName, fireInfo.FireID, fireInfo.Time, fireInfo.Description),false);
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // Back Button Event Handler
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}