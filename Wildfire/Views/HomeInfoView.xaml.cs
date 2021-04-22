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
       
        private void PreventionInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                preventionInfo.IsVisible = true;
                PreventionInfo.IsVisible = false;
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void PreventionRemovePopupTapped(object sender, EventArgs e)
        {
            preventionInfo.IsVisible = false;
            PreventionInfo.IsVisible = true;
        }

        private void DetectionInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                detectionInfo.IsVisible = true;
                DetectionInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void DetectionRemovePopupTapped(object sender, EventArgs e)
        {
            detectionInfo.IsVisible = false;
            DetectionInfo.IsVisible = true;
        }

        private void EvalutionInfo_Clicked(object sender, EventArgs e)
        {
            try
            {
                evaluationInfo.IsVisible = true;
                EvalutionInfo.IsVisible = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        void EvaluationRemovePopupTapped(object sender, EventArgs e)
        {
            evaluationInfo.IsVisible = false;
            EvalutionInfo.IsVisible = true;
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
    }
}