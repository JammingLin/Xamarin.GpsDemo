using System;
using Xamarin.GpsDemo.GPS;

namespace Xamarin.GpsDemo.Interfaces
{
    public interface IGpsProvider
    {
        void Initialize();
        void Start();
        void Stop();
        GpsData GpsData { get; }

        event Action<GpsData> GpsDataChanged;
    }
}
