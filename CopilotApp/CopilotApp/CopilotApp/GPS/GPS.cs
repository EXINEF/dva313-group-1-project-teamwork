using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CopilotApp
{
    public class GPS
    {
        //Async request coordinates from the android device internal GPS system
        public async static Task<Location> GetCoordinates()
        {
            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10)));
            return result;
        }

        public async static void UpdateCoordinates()
        {
            var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10)));

            if (result != null)
            {
                //GPSData.latitude = float.Parse(result.Latitude);
                //GPSData.longitude = result.Longitude;
            }
        }
    }
}
