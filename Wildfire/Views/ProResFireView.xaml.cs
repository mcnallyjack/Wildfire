/* Author:      Jack McNally
 * Page Name:   ProResfireView
 * Purpose:     Backend for Res Fire View.
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
    public partial class ProResFireView : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ProResFireView()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Loading Logic
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var fires = await firebaseHelper.GetResolvedFires();
            resFires.ItemsSource = fires;
        }

        // // List Item tap event handler
        private async void resFires_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await Task.Delay(50);
                var fireInfo = e.Item as Fire;
                await App.Current.MainPage.Navigation.PushModalAsync(new ProResSelectedView(fireInfo.PlaceName, fireInfo.FireID, fireInfo.Time, fireInfo.ResolvedDescription), false);
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