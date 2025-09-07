using Bluetooth__Finder.Models;
using Bluetooth__Finder.Services;
using Bluetooth__Finder.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bluetooth__Finder.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void OnOpenDevice(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is DeviceModel device)
                await Navigation.PushAsync(new DevicePage(new DeviceViewModel(
                    Services.GetRequiredService<BleService>(),
                    Services.GetRequiredService<AlarmService>(),
                    device)));
        }

        private async void OnSettings(object sender, EventArgs e)
            => await Navigation.PushAsync(new SettingsPage(
                new SettingsViewModel((BindingContext as MainViewModel)!)));
    }


}
