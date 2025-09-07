using Bluetooth__Finder.Models;
using Bluetooth__Finder.Services;
using Bluetooth__Finder.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Bluetooth__Finder.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly IServiceProvider _services;

        public MainPage(MainViewModel vm, IServiceProvider services)
        {
            InitializeComponent();
            BindingContext = vm;
            _services = services;
        }

        private async void OnOpenDevice(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is DeviceModel device)
                await Navigation.PushAsync(new DevicePage
                {
                    BindingContext = new DeviceViewModel(
                        _services.GetRequiredService<BleService>(),
                        _services.GetRequiredService<AlarmService>(),
                        device)
                });
        }

        private async void OnSettings(object sender, EventArgs e)
            => await Navigation.PushAsync(new SettingsPage
            {
                BindingContext = new SettingsViewModel((BindingContext as MainViewModel)!)
            });
    }
}