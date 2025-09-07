using Bluetooth__Finder.Models;
using Bluetooth__Finder.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bluetooth__Finder.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        readonly BleService _ble;
        readonly StorageService _storage;
        readonly LocationService _loc;

        [ObservableProperty] int gridSpan = 2;
        [ObservableProperty] bool isBusy;
        public ObservableCollection<DeviceModel> Devices { get; } = new();

        public MainViewModel(BleService ble, StorageService storage, LocationService loc)
        {
            _ble = ble; _storage = storage; _loc = loc;
            foreach (var d in _storage.Load()) Devices.Add(d);

            _ble.ConnectionChanged += async (s, e) =>
            {
                var d = Devices.FirstOrDefault(x => x.Id == e.deviceId);
                if (d == null) return;
                d.IsConnected = e.connected;
                if (!e.connected && d.AlarmEnabled)
                {
                    var (lat, lng) = await _loc.TryGetAsync();
                    d.LastDisconnects.Insert(0, new DisconnectLocation { When = DateTimeOffset.Now, Latitude = lat, Longitude = lng });
                    while (d.LastDisconnects.Count > 10) d.LastDisconnects.RemoveAt(d.LastDisconnects.Count - 1);
                    _storage.Save(Devices.ToList());
                }
            };
        }

        [RelayCommand]
        public async Task ScanAsync()
        {
            try
            {
                IsBusy = true;
                var found = await _ble.ScanAsync(TimeSpan.FromSeconds(8));
                foreach (var dev in found)
                {
                    if (Devices.Any(x => x.Id == dev.Id.ToString())) continue;
                    Devices.Add(new DeviceModel
                    {
                        Id = dev.Id.ToString(),
                        Name = string.IsNullOrWhiteSpace(dev.Name) ? "Unknown" : dev.Name!,
                        TileColor = RandomColor()
                    });
                }
                _storage.Save(Devices.ToList());
            }
            finally { IsBusy = false; }
        }

        Color RandomColor()
        {
            var colors = new[] { Colors.Red, Colors.Blue, Colors.LimeGreen, Colors.MediumPurple, Colors.Orange };
            return colors[new Random().Next(colors.Length)];
        }
    }
}
