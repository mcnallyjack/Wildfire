using System.Windows.Input;
using Xamarin.Forms;
using System;
using Xamarin.Essentials;
using System.Linq;
using Xamarin.Forms.GoogleMaps;

using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms.Xaml;
using Wildfire.Views;

using Wildfire.Helper;
using Xamarin.Forms.Internals;
using Wildfire.Services;

namespace Wildfire.Views
{
    public partial class SearchView : ContentPage
    {
        public static readonly BindableProperty FocusOriginCommandProperty =
           BindableProperty.Create(nameof(FocusOriginCommand), typeof(ICommand), typeof(SearchView), null, BindingMode.TwoWay);

        public ICommand FocusOriginCommand
        {
            get { return (ICommand)GetValue(FocusOriginCommandProperty); }
            set { SetValue(FocusOriginCommandProperty, value); }
        }

        public SearchView()
        {
            InitializeComponent();

        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                FocusOriginCommand = new Command(OnOriginFocus);
            }
        }

        void OnOriginFocus()
        {
            originEntry.Focus();
        }

        private async void searchPlace_Clicked(object sender, EventArgs e)
        {

            var search = originEntry.Text;
            var searchLocation = await Geocoding.GetLocationsAsync(search);
            var sourceLocations = searchLocation?.FirstOrDefault();
            Location sourceCoordinates = new Location(sourceLocations.Latitude, sourceLocations.Longitude);

            
            

            await Navigation.PushModalAsync(new MainTabPage());
           

        }
    }
}