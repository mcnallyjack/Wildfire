using System.Windows.Input;
using Xamarin.Forms;
using System;

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


            await Navigation.PushModalAsync(new MapView());


        }
    }
}