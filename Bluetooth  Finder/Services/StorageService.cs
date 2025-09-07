using Bluetooth__Finder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bluetooth__Finder.Services
{
    public class StorageService
    {
        const string KEY = "devices_state_v1";

        public void Save(List<DeviceModel> devices)
        {
            var json = JsonSerializer.Serialize(devices);
            Preferences.Set(KEY, json);
        }

        public List<DeviceModel> Load()
        {
            var json = Preferences.Get(KEY, "");
            return string.IsNullOrWhiteSpace(json)
                ? new List<DeviceModel>()
                : JsonSerializer.Deserialize<List<DeviceModel>>(json) ?? new();
        }
    }
}
