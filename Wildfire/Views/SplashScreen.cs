using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Wildfire.Views
{
    class SplashScreen : ContentPage
    {
        Image image;

        public SplashScreen()
        {
            var sub = new AbsoluteLayout();
            image = new Image
            {
                Source = "xhdpi.png",
                WidthRequest = 150,
                HeightRequest = 150
            };

            AbsoluteLayout.SetLayoutFlags(image,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(image,
                new Rectangle(0.5, 0.5, -1, -1));

            sub.Children.Add(image);

            this.BackgroundColor = Color.FromHex("#ffffff");
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await image.ScaleTo(1, 2000);
            await image.ScaleTo(0.9, 1500, Easing.Linear);
            await image.ScaleTo(150, 1200, Easing.Linear);
            await Navigation.PushModalAsync(new FirstPageView());
        }
    }
}
