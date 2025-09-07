using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluetooth__Finder.Models
{
    public class DisconnectLocation
    {
        public DateTimeOffset When { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Note { get; set; }
    }
}
