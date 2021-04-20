using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wildfire.Services;
using Wildfire.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wildfire
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabPage : TabbedPage
    {
        IAuth auth;
        public MainTabPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            CurrentPage = Children[1];
        }

        private async void SignOut_Clicked(object sender, EventArgs e)
        {
            var signOut = auth.SignOut();

            if (signOut)
            {
                await Navigation.PushModalAsync(new LoginPageView());
            }
        }
    }
}