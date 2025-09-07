using Bluetooth__Finder.Pages;
using Bluetooth__Finder.Services;
using Bluetooth__Finder.ViewModels;
using Plugin.Maui.Audio;

namespace Bluetooth__Finder.Droid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>();

            builder.Services.AddSingleton<BleService>();
            builder.Services.AddSingleton<LocationService>();
            builder.Services.AddSingleton<StorageService>();
            builder.Services.AddSingleton<IAudioManager, AudioManager>();
            builder.Services.AddSingleton<AlarmService>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
