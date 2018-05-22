using System;
namespace GrawApp.Model
{
    public class InputData
    {
        public double Time { get; set; }
        public double Pressure { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double WindDirection { get; set; }
        public double Geopotential { get; set; }
        public double Altitude { get; set; }
        public double Dewpoint { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public InputData()
        {
        }
    }
}
