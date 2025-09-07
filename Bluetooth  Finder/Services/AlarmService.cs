using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Maui.Audio;

namespace Bluetooth__Finder.Services
{
    public class AlarmService
    {
        readonly IAudioManager _audioManager;
        IAudioPlayer? _player;
        public AlarmService(IAudioManager audioManager) => _audioManager = audioManager;

        public async Task PlayAsync(string? filePathOrResource)
        {
            _player?.Stop();
            Stream stream;
            if (!string.IsNullOrEmpty(filePathOrResource) && File.Exists(filePathOrResource))
                stream = File.OpenRead(filePathOrResource);
            else
                stream = await FileSystem.OpenAppPackageFileAsync("alarm_default.mp3");
            _player = _audioManager.CreatePlayer(stream);
            _player.Loop = true;
            _player.Play();
        }
        public void Stop() => _player?.Stop();
    }
}
