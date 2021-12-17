using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CopilotApp
{
    public class GPS
    {
        public static double lastKnownLongitude = 0.0;
        public static double lastKnownLatitude = 0.0;

        //Async request coordinates from the android device internal GPS system
        public async static Task<Location> GetCoordinates()
        {
            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10)));

            lastKnownLongitude = result.Longitude;
            lastKnownLatitude = result.Latitude;

            return result;
        }

    }
}
