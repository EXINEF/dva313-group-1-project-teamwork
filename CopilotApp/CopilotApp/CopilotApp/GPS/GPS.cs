using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CopilotApp
{
    public class GPS
    {
        //Async request coordinate update from the android device internal GPS system
        public static async Task UpdateCoordinates(int maxGeolocationDelayms)
        {
            //GPS must be called from MainThread so we have to do this roundabout to be able to call it from an async thread.
            Device.BeginInvokeOnMainThread(async
            () =>
            {
                try
                {
                    var result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromMilliseconds(maxGeolocationDelayms)));

                    if (result != null)
                    {
                        Console.WriteLine($"Latitude: {result.Latitude}, Longitude: {result.Longitude}");
                        GPSData.latitude = result.Latitude;
                        GPSData.longitude = result.Longitude;
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    Console.WriteLine("GPS FeatureNotSupportedException: " + fnsEx.Message);
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    Console.WriteLine("GPS FeatureNotEnabledException: " + fneEx.Message);
                }
                catch (PermissionException pEx)
                {
                    Console.WriteLine("GPS PermissionException: " + pEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("GPS Exception: " + ex.Message);
                }

                await Task.CompletedTask;
            });

            await Task.CompletedTask;
        }
    }
}
