using System;
using System.Linq;
using Android.Locations;
using Android.OS;
using Android.Util;
using Xamarin.Forms;
using Xamarin.GpsDemo.GPS;
using Xamarin.GpsDemo.Interfaces;
using Application = Android.App.Application;

[assembly: Dependency(typeof(Xamarin.GpsDemo.Droid.GPS.GPSProvider))]
namespace Xamarin.GpsDemo.Droid.GPS
{
    public class GPSProvider : Java.Lang.Object, IGpsProvider, ILocationListener
    {
        private const string TAG = "Xamarin.GPSDemo";

        private LocationManager _locationManager;
        private string _locationProvider;

        #region IGpsProvider

        public GpsData GpsData { get; private set; }
        public event Action<GpsData> GpsDataChanged;

        public void Initialize()
        {

            _locationManager = (LocationManager)Application.Context.GetSystemService(global::Android.Content.Context.LocationService);
            var criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            var acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            _locationProvider = acceptableLocationProviders.Any() ? acceptableLocationProviders.First() : string.Empty;
            Log.Debug(TAG, "Using " + _locationProvider + ".");
        }

        public void Start()
        {
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        public void Stop()
        {
            _locationManager.RemoveUpdates(this);
        }

        #endregion


        #region ILocationListener

        public void OnLocationChanged(Location location)
        {
            if (location == null)
            {
                GpsData = null;
            }
            else
            {
                GpsData = new GpsData
                {
                    Longitude = location.Longitude,
                    Latitude = location.Latitude,
                    FixDateTime = location.Time.ToDateTime()
                };

                GpsDataChanged?.Invoke(GpsData);
            }
        }

        public void OnProviderDisabled(string provider)
        {
            Log.Debug(TAG, "Provider disabled: " + provider);
        }

        public void OnProviderEnabled(string provider)
        {
            Log.Debug(TAG, "Provider enabled: " + provider);
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            Log.Debug(TAG, $"Provider {provider} status: {status}");
        }

        #endregion

    }
}