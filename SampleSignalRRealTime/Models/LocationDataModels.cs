using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleSignalRRealTime.Models
{
    public class LocationDataModels
    {
        public string BrowserClientId { get; set; }
        public int? MarkerId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Username { get; set; }
    }

    public class LocationPosition
    {
        public double Top { get; set; }
        public double Left { get; set; }
    }

    public class ZoomLevel
    {
        public int ZoomValue { get; set; }
    }
}