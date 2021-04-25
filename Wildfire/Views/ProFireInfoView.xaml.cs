/* Author:      Jack McNally
 * Page Name:   ProFireInfoView
 * Purpose:     Backend for Info View fires.
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
    public partial class ProFireInfoView : ContentPage
    {
        public ProFireInfoView()
        {
            InitializeComponent();
        }

        // Current Fires Button Event Handler
        private async void CurrentFires_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new ProCurrentFiresView());
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }            
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

        // Resolved Button Event Handler
        private async void ResolvedFires_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushModalAsync(new ProResFireView());
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}