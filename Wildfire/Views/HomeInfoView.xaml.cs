/* Author:      Jack McNally
 * Page Name:   HomeInfoView
 * Purpose:     Backend for Home safety info.
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
    public partial class HomeInfoView : ContentPage
    {
        public HomeInfoView()
        {
            InitializeComponent();
        }
       
        // Prevention Button Handler
        private void PreventionInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                FrameLabel.IsVisible = false;
                FrameLabel1.IsVisible = false;
                preventionInfo.IsVisible = true;
                PreventionInfo.IsVisible = false;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // Prevention Button Remove Handler
        void PreventionRemovePopupTapped(object sender, EventArgs e)
        {
            FrameLabel.IsVisible = true;
            FrameLabel1.IsVisible = true;
            preventionInfo.IsVisible = false;
            PreventionInfo.IsVisible = true;
        }

        // Detection Button Handler
        private void DetectionInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                FrameLabel.IsVisible = false;
                FrameLabel1.IsVisible = false;
                detectionInfo.IsVisible = true;
                DetectionInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // Detection Button Remove Handler
        void DetectionRemovePopupTapped(object sender, EventArgs e)
        {
            FrameLabel.IsVisible = true;
            FrameLabel1.IsVisible = true;
            detectionInfo.IsVisible = false;
            DetectionInfo.IsVisible = true;
        }

        // Evacuation Button Handler
        private void EvalutionInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                FrameLabel.IsVisible = false;
                FrameLabel1.IsVisible = false;
                evaluationInfo.IsVisible = true;
                EvalutionInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        // Evacuation Button Remove Handler
        void EvaluationRemovePopupTapped(object sender, EventArgs e)
        {
            FrameLabel.IsVisible = true;
            FrameLabel1.IsVisible = true;
            evaluationInfo.IsVisible = false;
            EvalutionInfo.IsVisible = true;
        }
  
        // Back Button Handler
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
    }
}