using System;

namespace Xamarin.GpsDemo
{
    public static class Extensions
    {
        public static DateTime ToDateTime(this long @this)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(@this);
        }
    }
}
