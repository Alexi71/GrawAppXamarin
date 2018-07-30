using System;
using System.Collections.Generic;
using System.Linq;
using GrawApp.Model;

namespace GrawApp.Controller.Raw
{
    public class RawDataRepository
    {
        public List<RawDataValue> DataList { get; set; }

        public RawDataRepository()
        {
            GetRepository();
        }

        void GetRepository()
        {
            DataList = new List<RawDataValue>();
            DataList.Add(new RawDataValue
            {
                Type = DataType.Pressure,
                ImageName = "icons8_pressure_filled_50.png",
                HeaderName = "Pressure",
                Value = "100 mB"
            });

            DataList.Add(new RawDataValue
            {
                Type = DataType.Temperature,
                ImageName = "icons8_temperature_48.png",
                HeaderName = "Temperature",
                Value = "15.0 °C"
            });

            DataList.Add(new RawDataValue
            {
                Type = DataType.Humdity,
                ImageName = "icons8_water_50.png",
                HeaderName = "Humidity",
                Value = "70 %"
            });

            DataList.Add(new RawDataValue
            {
                Type = DataType.Windspeed,
                ImageName = "icons8_fan_filled_50.png",
                HeaderName = "Wind speed",
                Value = "3.0 m/s"
            });

            DataList.Add(new RawDataValue
            {
                Type = DataType.Winddirection,
                ImageName = "icons8_windsock_filled_50.png",
                HeaderName = "Wind direction",
                Value = "200 °"
            });

            DataList.Add(new RawDataValue
            {
                Type = DataType.Altitude,
                ImageName = "vertical_double_arrow.png",
                HeaderName = "Altitude",
                Value = "1000 m"
            });
        }

        public RawDataValue GetItem(int index)
        {
            if (index >= DataList.Count)
            {
                return null;
            }

            return DataList[index];
        }

        public void SetValue(double value,DataType type,string format,string unit)
        {
            var newValue = DataList.FirstOrDefault(s => s.Type == type);
            newValue.Value = $"{value.ToString(format)} {unit}";
        }

    }
}
