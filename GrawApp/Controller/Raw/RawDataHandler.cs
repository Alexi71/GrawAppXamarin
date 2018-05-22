using System;
using System.Collections.Generic;
using Firebase.Database;
using Foundation;
using GrawApp.Model;

namespace GrawApp.Controller.Raw
{
    public class RawDataHandler
    {
        List<InputData> DataList { get; set; }

        public string StationKey { get; set; }
        public string FlightKey { get; set; }
        public RawDataHandler()
        {
            DataList = new List<InputData>();
        }

        public void InitializeDataBase()
        {
            var rootNode = Firebase.Database.Database.DefaultInstance.GetRootReference();
            var childNode = rootNode.GetChild("station").GetChild(StationKey).
                                    GetChild("flights").
                                    GetChild(FlightKey).GetChild("rawdata");
            var referenceNode = childNode.ObserveEvent(DataEventType.ChildAdded, (snapshot, prevKey) =>
            {
                var data = snapshot.GetValue<NSDictionary>();
                Console.WriteLine(snapshot.Key);


                //FlightContent flightData = new FlightContent();
                //flightData.Key = snapshot.Key;
                //if (data["Date"] != null)
                //    flightData.Date = Convert.ToString(data["Date"]);
                //if (data["Time"] != null)
                //    flightData.Time = Convert.ToString(data["Time"]);
                //if (data["FileName"] != null)
                //    flightData.FileName = Convert.ToString(data["FileName"]);
                //if (data["Url"] != null)
                //    flightData.Url = Convert.ToString(data["Url"]);
                //if (data["Url100"] != null)
                //    flightData.Url100 = Convert.ToString(data["Url100"]);
                //if (data["UrlEnd"] != null)
                //    flightData.UrlEnd = Convert.ToString(data["UrlEnd"]);
                //if (data["IsRealTimeDataAvailable"] != null)
                //{
                //    if (data["IsRealTimeDataAvailable"] is NSNumber)
                //    {
                //        var x = (NSNumber)data["IsRealTimeDataAvailable"];
                //        var ob = x.BoolValue;
                //        flightData.IsRealTimeFlight = Convert.ToBoolean(ob);
                //    }


                //}

            });
        }
    }
}
