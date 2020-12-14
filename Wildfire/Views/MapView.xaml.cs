using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();
           
           
            Pin pinCarlow = new Pin()
            {
                Type = PinType.Place,
                Label = "Carlow",
                Address = "Carlow, Ireland",
                Position = new Position(52.8365d, -6.9341d),

                Tag = "id_Tokyo"
            };
            map.Pins.Add(pinCarlow);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pinCarlow.Position, Distance.FromMeters(5000)));
        }

       

        private void Search_Clicked(object sender, EventArgs e)
        {
           
            popupSearch.IsVisible = true;
        }

        void RemovePopupTapped(object sender, EventArgs e)
        {
            popupSearch.IsVisible = false;
        }
    }
}