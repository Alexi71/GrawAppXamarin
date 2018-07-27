using System;
namespace GrawApp.Model
{
    public enum DataType {
        Pressure,
        Temperature,
        Humdity,
        Windspeed,
        Winddirection,
        Altitude
    }
    public class RawDataValue
    {
        public DataType Type { get; set; }
        public string ImageName { get; set; }
        public string HeaderName { get; set; }
        public string Value { get; set; }

        public RawDataValue()
        {
        }
    }
}
