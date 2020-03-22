using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GeoJSON.Net.Feature;
using MoreLinq;
using Newtonsoft.Json;
using StayAtHoome.Data;
using StayAtHoome.Models;
using Xamarin.Forms;
using Point = GeoJSON.Net.Geometry.Point;

namespace StayAtHoome.Services
{
    public class DemoService
    {
        public async Task LoadDemoData()
        {
            var locationRepo = DependencyService.Get<LocationRecordRepository>();
            var count = await locationRepo.Count();
            // if (count > 20) return;

            var assembly = Assembly.GetAssembly(this.GetType());
            var stream = assembly
                .GetManifestResourceStream("StayAtHoome.DemoData.track_points.json");
            if (stream == null)
            {
                throw new Exception("Demo Data not found.");
            }

            using (var streamReader = new StreamReader(stream))
            {
                var json = streamReader.ReadToEnd();
                var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(json);

                var homeFeature = featureCollection.Features.FirstOrDefault();
                var homePoint = homeFeature?.Geometry as Point;
                if (homePoint == null) return;

                var userRepo = DependencyService.Get<UserRepository>();
                var user = await userRepo.GetUserAsync() ?? new User
                {
                    HomeAccuracy = 10,
                    HomeLatitude = homePoint.Coordinates.Latitude,
                    HomeLongitude = homePoint.Coordinates.Longitude,
                };
                await userRepo.UpdateUserAsync(user);
                Console.WriteLine($"Updated demo user with home location {homePoint.Coordinates}");

                await locationRepo.Clear();

                foreach (var feature in featureCollection.Features.TakeEvery(20))
                {
                    var timeString = feature.Properties.ContainsKey("time")
                        ? feature.Properties["time"] as string
                        : null;
                    var time = timeString != null ? DateTimeOffset.Parse(timeString) as DateTimeOffset? : null;
                    var point = feature.Geometry as Point;

                    if (time != null && point != null)
                    {
                        await locationRepo.CreateLocationRecord(new LocationRecord
                        {
                            Accuracy = 10,
                            Latitude = point.Coordinates.Latitude,
                            Longitude = point.Coordinates.Longitude,
                            Timestamp = time.Value,
                        });
                        Console.WriteLine($"Added tracked demo location at {time.Value}");
                    }
                }
            }
        }
    }
}