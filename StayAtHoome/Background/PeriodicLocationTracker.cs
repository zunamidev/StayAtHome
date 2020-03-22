using System;
using System.Threading.Tasks;
using StayAtHoome.Data;
using StayAtHoome.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StayAtHoome.Background
{
    public class PeriodicLocationTracker
    {
        public async Task<bool> Execute()
        {
            var repo = new LocationRecordRepository();

            Console.WriteLine("Starting location tracking");
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    Console.WriteLine(
                        $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    await repo.CreateLocationRecord(new LocationRecord
                    {
                        Accuracy = location.Accuracy,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Timestamp = location.Timestamp
                    });
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine(fnsEx);
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Console.WriteLine(fneEx);
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine(pEx);
                // Handle permission exception
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Unable to get location
            }

            return true;
        }
    }
}
