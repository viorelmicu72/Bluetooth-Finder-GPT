using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bluetooth__Finder.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        readonly MainViewModel _main;
        [ObservableProperty] int gridSpan;
        [ObservableProperty] string? alarmFilePath;

        public SettingsViewModel(MainViewModel main)
        { _main = main; gridSpan = _main.GridSpan; }

        [RelayCommand]
        public void Apply()
        {
            _main.GridSpan = GridSpan;
        }

        [RelayCommand]
        public async Task PickAlarmAsync()
        {
            var res = await FilePicker.Default.PickAsync(new PickOptions { PickerTitle = "Alege melodia pentru alarmă" });
            if (res != null) AlarmFilePath = res.FullPath;
        }
    }
}
