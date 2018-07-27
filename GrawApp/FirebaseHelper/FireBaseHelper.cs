using System;
using Firebase.Database;
using Firebase.Storage;
using Foundation;
using GrawApp.Model;

namespace GrawApp.FirebaseHelper
{
    public class FireBaseHelper
    {

        private Firebase.Database.Database _firebaseDatabase;
        private readonly string _stationKey;
        private readonly string _flightKey;
        Action<RawData> _action;

        public FireBaseHelper()
        {
        }

        public FireBaseHelper(string stationKey,string flightKey)
        {
            _stationKey = stationKey;
            _flightKey = flightKey;
        }

        public void StartRawDataObserver(Action<RawData> action)
        {
            _action = action;
            var rootNode = Firebase.Database.Database.DefaultInstance.GetRootReference();
            var childNode = rootNode.GetChild("station").GetChild(_stationKey).GetChild("flights")
                                    .GetChild(_flightKey).GetChild("rawdata");
            /* .GetQueryOrderedByChild("EpochTime")
             .GetQueryStartingAtValue(NSObject.FromObject(_startTime.GetUnixEpoch()))
             .GetQueryEndingAtValue(NSObject.FromObject(_endTime.GetUnixEpoch()));*/

            var referenceNode = childNode.ObserveEvent(DataEventType.ChildAdded, (snapshot, prevKey) =>
            {
                var flightData = GetRawDataFromSnapshot(snapshot);

            });
        }

        private RawData GetRawDataFromSnapshot(DataSnapshot snapshot)
        {
            var child = snapshot.Children;
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
            //if (data["EpochTime"] != null)
            //{
            //    if (data["EpochTime"] is NSNumber)
            //    {
            //        var x = (NSNumber)data["EpochTime"];
            //        var ob = x.DoubleValue;
            //        flightData.EpochTime = Convert.ToDouble(ob);
            //    }
            //}
            return new RawData();
        }

        public static void DeleteFlightFromCloud(FlightContent data, string stationKey)
        {

            var storage = Storage.DefaultInstance;
            if (!string.IsNullOrEmpty(data.Url))
            {


                var urlDeleted = storage.GetReferenceFromUrl(data.Url);
                urlDeleted.Delete((error) =>
                {
                    if (error != null)
                    {
                        // Uh-oh, an error occurred!
                        Console.WriteLine($"Delete file :{data.Url} failed");
                    }
                    else
                    {
                        // File deleted successfully
                        Console.WriteLine($"Delete file :{data.Url} was successfully");
                    }
                });
            }
            if (!string.IsNullOrEmpty(data.Url100))
            {
                var url100Deleted = storage.GetReferenceFromUrl(data.Url100);
                url100Deleted.Delete((error) =>
                {
                    if (error != null)
                    {
                        // Uh-oh, an error occurred!
                        Console.WriteLine($"Delete file :{data.Url100} failed");
                    }
                    else
                    {
                        // File deleted successfully
                        Console.WriteLine($"Delete file :{data.Url100} was successfully");
                    }
                });
            }

            if (!string.IsNullOrEmpty(data.UrlEnd))
            {
                var urlEndDeleted = storage.GetReferenceFromUrl(data.UrlEnd);
                urlEndDeleted.Delete((error) =>
                {
                    if (error != null)
                    {
                            // Uh-oh, an error occurred!
                        Console.WriteLine($"Delete file :{data.UrlEnd} failed");
                        }
                    else
                    {
                            // File deleted successfully
                        Console.WriteLine($"Delete file :{data.UrlEnd} was successfully");
                        }
                });
            }
                                        
            var rootNode = Firebase.Database.Database.DefaultInstance.GetRootReference();
            rootNode.GetChild("station").GetChild(stationKey)
                    .GetChild("flights").GetChild(data.Key).RemoveValue((error, reference) =>
            {
                if (error != null)
                {
                    //do something
                    Console.WriteLine($"Delete flight with key:{data.Key} failed");
                }
                else
                {
                    Console.WriteLine($"Delete flight with key:{data.Key} was successfully");
                }
            });


        }
    }
}
