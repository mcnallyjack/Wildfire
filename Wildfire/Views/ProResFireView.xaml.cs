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

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var fires = await firebaseHelper.GetResolvedFires();
            resFires.ItemsSource = fires;
        }

        private async void resFires_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Task.Delay(500);
            var fireInfo = e.Item as Fire;
            await App.Current.MainPage.Navigation.PushModalAsync(new ProResSelectedView(fireInfo.PlaceName, fireInfo.FireID, fireInfo.Time, fireInfo.ResolvedDescription));

        }

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