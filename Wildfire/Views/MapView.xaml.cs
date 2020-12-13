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
            Pin pinTokyo = new Pin()
            {
                Type = PinType.Generic,
                Label = "Tokyo",
                Address = "Sumida-ku, Tokyo, Japan",
                Position = new Position(35.71d, 139.81d),
                
                Tag = "id_Tokyo"
            };
            map.Pins.Add(pinTokyo);
            Pin pinCarlow = new Pin()
            {
                Type = PinType.Place,
                Label = "Carlow",
                Address = "Carlow, Ireland",
                Position = new Position(52.8365d, -6.9341d),

                Tag = "id_Tokyo"
            };
            map.Pins.Add(pinCarlow);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pinTokyo.Position, Distance.FromMeters(5000)));
        }

       

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SearchView());
        }

       
    }
}