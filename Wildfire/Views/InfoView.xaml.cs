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
    }
}