/* Author:      Jack McNally
 * Page Name:   WildlabdInfoView
 * Purpose:     Backend for Wildland safety info.
 */
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

        // Back Button Event Handler
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

        // Before Button Event Handler
        private void BeforeFireInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                FrameLabel.IsVisible = false;
                FrameLabel1.IsVisible = false;
                beforeInfo.IsVisible = true;
                BeforeFireInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // Before Remove Event Handler
        void BeforeRemovePopupTapped(object sender, EventArgs e)
        {
            FrameLabel.IsVisible = true;
            FrameLabel1.IsVisible = true;
            beforeInfo.IsVisible = false;
            BeforeFireInfo.IsVisible = true;
        }

        // During Button Event Handler
        private void DuringFireInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                FrameLabel.IsVisible = false;
                FrameLabel1.IsVisible = false;
                duringInfo.IsVisible = true;
                DuringFireInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // During Remove Event Handler
        void DuringRemovePopupTapped(object sender, EventArgs e)
        {
            FrameLabel.IsVisible = true;
            FrameLabel1.IsVisible = true;
            duringInfo.IsVisible = false;
            DuringFireInfo.IsVisible = true;
        }

        // After Button Event Handler
        private void AfterFireInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                FrameLabel.IsVisible = false;
                FrameLabel1.IsVisible = false;
                afterInfo.IsVisible = true;
                AfterFireInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // After Remove Event Handler
        void AfterRemovePopupTapped(object sender, EventArgs e)
        {
            FrameLabel.IsVisible = true;
            FrameLabel1.IsVisible = true;
            afterInfo.IsVisible = false;
            AfterFireInfo.IsVisible = true;
        }
    }
}