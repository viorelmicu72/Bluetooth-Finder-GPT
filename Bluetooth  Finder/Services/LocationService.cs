using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace Bluetooth__Finder.Services
{
    public class LocationService
    {
        public async Task<(double? lat, double? lng)> TryGetAsync()
        {
            var loc = await Geolocation.GetLastKnownLocationAsync()
                      ?? await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best));
            return loc is null ? (null, null) : (loc.Latitude, loc.Longitude);
        }
    }
}
