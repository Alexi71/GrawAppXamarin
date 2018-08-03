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
        Action<FlightContent> _actionFlightContent;
        Action<FlightContent> _actionDeleteFlight;
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
            var data = GetRawDataFromSnapshot(snapshot);
                _action?.Invoke(data);
            });
        }

        public void StartStationStatusObserver(Action<FlightContent> action,string stationKey)
        {
            _actionFlightContent = action;

            var rootNode = Firebase.Database.Database.DefaultInstance.GetRootReference();
            var childNode = rootNode.GetChild("station").GetChild(stationKey).GetChild("flights")
                                    .GetQueryOrderedByChild("EpochTime").GetQueryLimitedToLast(1);
            childNode.ObserveSingleEvent(DataEventType.ChildAdded, (snapshot, prevKey) =>
            {

                var data = GetFlightDataFromSnapshot(snapshot);
                _actionFlightContent?.Invoke(data);
            });
        }


        public void StartFlightFromStationObserver(Action<FlightContent> action, 
                                                   Action<FlightContent> deleteAction,
                                                   string stationKey,
                                                  DateTime startTime, DateTime endTime)
        {
            _actionFlightContent = action;
            _actionDeleteFlight = deleteAction;
            var rootNode = Firebase.Database.Database.DefaultInstance.GetRootReference();
            var childNode = rootNode.GetChild("station").GetChild(stationKey).GetChild("flights");
                                   /* .GetQueryOrderedByChild("EpochTime")
                                    .GetQueryStartingAtValue(NSObject.FromObject(_startTime.GetUnixEpoch()))
                                    .GetQueryEndingAtValue(NSObject.FromObject(_endTime.GetUnixEpoch()));*/
            
            childNode.ObserveEvent(DataEventType.ChildAdded, (snapshot, prevKey) =>
            {

                var data = GetFlightDataFromSnapshot(snapshot);
                _actionFlightContent?.Invoke(data);
            });

            var deleteNode = childNode.ObserveEvent(DataEventType.ChildRemoved, (snapshot, prevKey) =>
            {
                var flightData = GetFlightDataFromSnapshot(snapshot);
                _actionDeleteFlight?.Invoke(flightData);
            });
        }





        private FlightContent GetFlightDataFromSnapshot(DataSnapshot snapshot)
        {
            var data = snapshot.GetValue<NSDictionary>();
            Console.WriteLine(snapshot.Key);
            FlightContent flightData = new FlightContent();
            flightData.Key = snapshot.Key;
            flightData.Date = GetStringValue(data["Date"]);
            flightData.Time = GetStringValue(data["Time"]);
            flightData.FileName = GetStringValue(data["FileName"]);
            flightData.Url = GetStringValue(data["Url"]);
            flightData.Url100 = GetStringValue(data["Url100"]);
            flightData.UrlEnd = GetStringValue(data["UrlEnd"]);
            flightData.IsRealTimeFlight = GetBoolValue(data["IsRealTimeDataAvailable"]).GetValueOrDefault();
            flightData.EpochTime = GetDoubleValue(data["EpochTime"]).GetValueOrDefault();
            return flightData;
        }


        private RawData GetRawDataFromSnapshot(DataSnapshot snapshot)
        {
            var child = snapshot.Children;
            var data = snapshot.GetValue<NSDictionary>();
            Console.WriteLine(snapshot.Key);
            var rawdata = new RawData();
            rawdata.EpochTime = GetDoubleValue(data["EpochTime"]).GetValueOrDefault();
            rawdata.Latitude = GetDoubleValue(data["Latitude"]).GetValueOrDefault();
            rawdata.Longitude = GetDoubleValue(data["Longitude"]).GetValueOrDefault();
            rawdata.Temperature = GetDoubleValue(data["Temperature"]).GetValueOrDefault();
            rawdata.Humdity = GetDoubleValue(data["Humidity"]).GetValueOrDefault();
            rawdata.Pressure = GetDoubleValue(data["Pressure"]).GetValueOrDefault();
            rawdata.Altitude = GetDoubleValue(data["Altitude"]).GetValueOrDefault();
            rawdata.WindSpeed = GetDoubleValue(data["WindSpeed"]).GetValueOrDefault();
            rawdata.WindDirection = GetDoubleValue(data["WinDirection"]).GetValueOrDefault();
            rawdata.GpsStatus = GetIntegerValue(data["GpsStatus"]).GetValueOrDefault();
            rawdata.SensorStatus = GetIntegerValue(data["SensorStatus"]).GetValueOrDefault();
            rawdata.TelemetryStatus = GetIntegerValue(data["TelemetryStatus"]).GetValueOrDefault();
            rawdata.StartTime = GetDoubleValue(data["StartTimeEpoch"]).GetValueOrDefault();
            rawdata.StartDetected = GetBoolValue(data["StartDetected"]).GetValueOrDefault();
            rawdata.Url = GetStringValue(data["Url"]);
            return rawdata;
        }

        string GetStringValue (NSObject data)
        {
            if (data != null)
            {
                return Convert.ToString(data);
            }
            return string.Empty;
        }

        bool? GetBoolValue(NSObject data)
        {
            if (data != null)
            {
                if (data is NSNumber x)

                {
                    return x.BoolValue;

                }
            }
            return null;
        }


        double? GetDoubleValue(NSObject data)
        {
            if (data != null)
            {
                if (data is NSNumber x)

                {
                    return x.DoubleValue;

                }
            }
            return null;
        }

        int? GetIntegerValue(NSObject data)
        {
            if (data != null)
            {
                if (data is NSNumber x)

                {
                    return x.Int32Value;

                }
            }
            return null;
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
