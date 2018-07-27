using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace GrawApp.Model
{
    public class RawData
    {
        public double EpochTime { get; set; }
        public double Pressure { get; set; }
        public double Temperature { get; set; }
        public double Humdity { get; set; }
        public double WindSpeed { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double WindDirection { get; set; }
        public double Altitude { get; set; }
        public int GpsStatus { get; set; }
        public int SensorStatus { get; set; }
        public int TelemetryStatus { get; set; }
        public bool StartDetected { get; set; }
        public double StartTime { get; set; }
        public string Url { get; set; }




    }
}