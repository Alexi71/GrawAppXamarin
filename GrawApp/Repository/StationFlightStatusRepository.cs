using System;
using System.Collections.ObjectModel;
using GrawApp.Database;
using GrawApp.Helper;
using GrawApp.Model;

namespace GrawApp.Repository
{
    public class StationFlightStatusRepository
    {
        public ObservableCollection<StationFlightInfo> FlightStatusList { get; set; }

        public StationFlightStatusRepository()
        {
            FlightStatusList = new ObservableCollection<StationFlightInfo>();
            //GenerateData();
        }

        public string GetStation(int rowIndex) => FlightStatusList[rowIndex].Name;
       

        public StationFlightInfo GetFlightInfo(int rowIndex) => FlightStatusList[rowIndex];


        public void Add(Station station, FlightContent flight)
        {
            FlightStatusList.Add(new StationFlightInfo()
            {
                Name = station.Name,
                Date = $"{flight.EpochTime.FromUnixTime():dd.MM.yyyy HH:mm}",
                Status = GetImageName(flight.EpochTime.FromUnixTime(), flight.IsRealTimeFlight),
                Station = station
            });
        }

        public void Delete(int index)
        {
            FlightStatusList.RemoveAt(index);
        }

        public void Clear()
        {
            FlightStatusList.Clear();
        }

        string GetImageName(DateTime date, bool isRealTimeFlight)
        {

            if (isRealTimeFlight)
            {
                return "g5_status_32x32_gruen_heller.png";
            }
            var now = DateTime.UtcNow;
            var span = now - date;
            if (span.Days == 0 || span.Days <= 2)
            {
                return "g5_status_32x32_grau.png";
            }
            else
            {
                return "g5_status_32x32_rot.png";
            }
        }




    }
}
