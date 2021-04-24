using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wildfire.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WildlandInfoView : ContentPage
    {
        public WildlandInfoView()
        {
            InitializeComponent();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void BeforeFireInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                beforeInfo.IsVisible = true;
                BeforeFireInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void BeforeRemovePopupTapped(object sender, EventArgs e)
        {
            beforeInfo.IsVisible = false;
            BeforeFireInfo.IsVisible = true;
        }

        private void DuringFireInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                duringInfo.IsVisible = true;
                DuringFireInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void DuringRemovePopupTapped(object sender, EventArgs e)
        {
            duringInfo.IsVisible = false;
            DuringFireInfo.IsVisible = true;
        }

        private void AfterFireInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                afterInfo.IsVisible = true;
                AfterFireInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void AfterRemovePopupTapped(object sender, EventArgs e)
        {
            afterInfo.IsVisible = false;
            AfterFireInfo.IsVisible = true;
        }
    }
}