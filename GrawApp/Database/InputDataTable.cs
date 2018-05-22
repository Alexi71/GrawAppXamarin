using System;
using System.Collections.Generic;
using GrawApp.Model;
using SQLite;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace GrawApp.Database
{
    public class FlightDataTable:FlightContent

    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        //public string Key { get; set; }
        //public string Date { get; set; }
        //public string Time { get; set; }
        //public string FileName { get; set; }
        //public string Url { get; set; }
        //public string Url100 { get; set; }
        //public string UrlEnd { get; set; }
        //public bool IsRealTimeFlight { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<InputDataTable> InputDataList { get; set; }

        public void Add(FlightContent item)
        {
            Key = item.Key;
            Date = item.Date;
            Time = item.Time;
            FileName = item.FileName;
            Url = item.Url;
            Url100 = item.Url100;
            UrlEnd = item.UrlEnd;
            IsRealTimeFlight = item.IsRealTimeFlight;
        }
    }

    public class InputDataTable:InputData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //public double Time { get; set; }
        //public double Pressure { get; set; }
        //public double Temperature { get; set; }
        //public double Humidity { get; set; }
        //public double WindSpeed { get; set; }
        //public double WindDirection { get; set; }
        //public double Geopotential { get; set; }
        //public double Altitude { get; set; }
        //public double Dewpoint { get; set; }
        //public double Latitude { get; set; }
        //public double Longitude { get; set; }
        [ForeignKey(typeof(FlightDataTable))]
        public int FlightId { get; set; }

        public InputDataTable()
        {
        }

        public void Add(InputData item)
        {
            Time = item.Time;
            Pressure = item.Pressure;
            Temperature = item.Temperature;
            Humidity = item.Humidity;
            WindSpeed = item.WindSpeed;
            WindDirection = item.WindDirection;
            Geopotential = item.Geopotential;
            Altitude = item.Altitude;
            Dewpoint = item.Dewpoint;
            Latitude = item.Latitude;
            Longitude = item.Longitude;
        }
    }
}
