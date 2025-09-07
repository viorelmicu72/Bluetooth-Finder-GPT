using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Bluetooth__Finder.Models
{
    //internal class DeviceModel
    //{
    //}
    public class DeviceModel
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "Unknown";
        public Color TileColor { get; set; } = Colors.SlateBlue;
        public bool AlarmEnabled { get; set; } = true;
        public bool IsConnected { get; set; }
        public ObservableCollection<DisconnectLocation> LastDisconnects { get; set; } = new();
    }
}
