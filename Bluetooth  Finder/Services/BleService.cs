using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;

namespace Bluetooth__Finder.Services
{
    public class BleService
    {
        readonly IAdapter _adapter = CrossBluetoothLE.Current.Adapter;

        public event EventHandler<(string deviceId, bool connected)>? ConnectionChanged;
        public IDevice? GetConnectedById(string id) =>
            _adapter.ConnectedDevices.FirstOrDefault(d => d.Id.ToString() == id);

        public async Task<IEnumerable<IDevice>> ScanAsync(TimeSpan timeout)
        {
            var found = new List<IDevice>();
            _adapter.DeviceDiscovered += (s, e) => { if (!found.Any(d => d.Id == e.Device.Id)) found.Add(e.Device); };
            await _adapter.StartScanningForDevicesAsync(timeout: timeout);
            return found;
        }

        public async Task<bool> ConnectAsync(IDevice device)
        {
            _adapter.DeviceConnected += OnConnected;
            _adapter.DeviceDisconnected += OnDisconnected;
            await _adapter.ConnectToDeviceAsync(device);
            return true;
        }

        void OnConnected(object? s, DeviceEventArgs e) =>
            ConnectionChanged?.Invoke(this, (e.Device.Id.ToString(), true));

        void OnDisconnected(object? s, DeviceEventArgs e) =>
            ConnectionChanged?.Invoke(this, (e.Device.Id.ToString(), false));

        public async Task<bool> TryRingUsingIASAsync(IDevice device)
        {
            // Immediate Alert Service (0x1802), Alert Level (0x2A06) – 0x02 = High Alert
            var ias = await device.GetServiceAsync(Guid.Parse("00001802-0000-1000-8000-00805f9b34fb"));
            if (ias == null) return false;
            var alertChar = await ias.GetCharacteristicAsync(Guid.Parse("00002a06-0000-1000-8000-00805f9b34fb"));
            if (alertChar == null || !alertChar.CanWrite) return false;
            await alertChar.WriteAsync(new byte[] { 0x02 });
            return true;
        }
    }
}
