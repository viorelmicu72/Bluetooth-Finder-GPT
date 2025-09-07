using Bluetooth__Finder.Models;
using Bluetooth__Finder.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluetooth__Finder.ViewModels
{
    public partial class DeviceViewModel : ObservableObject
    {
        readonly BleService _ble;
        readonly AlarmService _alarm;
        public DeviceModel Device { get; }

        public DeviceViewModel(BleService ble, AlarmService alarm, DeviceModel device)
        { _ble = ble; _alarm = alarm; Device = device; }

        [RelayCommand]
        public async Task RingAsync()
        {
            // 1) Încearcă să sune dispozitivul prin IAS; dacă nu merge, sună telefonul
            var dev = _ble.GetConnectedById(Device.Id);
            if (dev != null && await _ble.TryRingUsingIASAsync(dev)) return;

            await _alarm.PlayAsync(null);
            await Task.Delay(5000);
            _alarm.Stop();
        }
    }
}
