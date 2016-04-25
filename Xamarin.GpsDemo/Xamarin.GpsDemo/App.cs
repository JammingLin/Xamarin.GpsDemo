using System;
using Xamarin.Forms;
using Xamarin.GpsDemo.GPS;
using Xamarin.GpsDemo.Interfaces;

namespace Xamarin.GpsDemo
{
    public class App : Application
    {
        private readonly Label _gpsDataLabel;

        public App()
        {
            var gpsProvider = DependencyService.Get<IGpsProvider>();
            if (gpsProvider != null)
            {
                gpsProvider.Initialize();
                gpsProvider.Start();
                gpsProvider.GpsDataChanged += GPSProvider_GpsDataChanged;
            }

            _gpsDataLabel = new Label
            {
                XAlign = TextAlignment.Center,
                Text = "Welcome to Xamarin Forms!",
                FontSize = 30
            };
            // The root page of your application
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        _gpsDataLabel
                    }
                }
            };
        }

        private void GPSProvider_GpsDataChanged(GpsData gpsData)
        {
            try
            {
                _gpsDataLabel.Text = $"{gpsData.FixDateTime}: {gpsData.Latitude}, {gpsData.Longitude}";
            }
            catch (Exception ex)
            {
                _gpsDataLabel.Text = ex.Message;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
