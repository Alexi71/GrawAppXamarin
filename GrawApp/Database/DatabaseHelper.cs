using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GrawApp.Model;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;

namespace GrawApp.Database
{
    public class DatabaseHelper
    {
        public DatabaseHelper()
        {
        }

        public static bool Insert<T>(ref T data)
        {
            using(var conn = new SQLiteConnection(new SQLitePlatformIOS(),GetPath()))
            {
                conn.CreateTable<T>();

                if(conn.Insert(data) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool InsertStation(Station data)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(),GetPath()))
            {
                conn.CreateTable<Station>();
                var station = conn.Table<Station>().FirstOrDefault(x => x.Key == data.Key);
                if(station == null)
                {
                    if (conn.Insert(data) != 0)
                    {
                        return true;
                    }
                }
                else
                {
                    conn.Update(data);
                    return true;
                }

            }
            return false;
        }

        public static FlightDataTable GetFlightData(string key)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(), GetPath()))
            {
                CreateFlightTables(conn);
                var data = conn.GetAllWithChildren<FlightDataTable>().
                                         FirstOrDefault(x => x.Key == key);
                
                if (data?.InputDataList != null)
                {
                    
                    //var outputList = new List<InputData>(data.InputDataList);
                    return data;
                }
                return null;
            }
        }

        private static void CreateFlightTables(SQLiteConnection conn)
        {
            conn.CreateTable<FlightDataTable>();
            conn.CreateTable<InputDataTable>();
        }

        public static FlightDataTable InsertFlight(FlightContent item,List<InputData> dataList)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(), GetPath()))
            {
                CreateFlightTables(conn);
                var flightData = conn.GetAllWithChildren<FlightDataTable>().
                                     FirstOrDefault(x => x.Key == item.Key);
                var data = new FlightDataTable();
                data.Add(item);
                if(dataList != null)
                {
                    List<InputDataTable> inputTable = new List<InputDataTable>();
                    foreach (var inputItem in dataList)
                    {
                        var value = new InputDataTable();
                        value.Add(inputItem);
                        inputTable.Add(value);
                    }
                    data.InputDataList = inputTable;
                }
                if (flightData == null)
                {
                    conn.InsertWithChildren(data, true);
                    return data;

                }
                else
                {
                    conn.UpdateWithChildren(data);
                    return data;
                }

            }
            //return false;
        }

        public static bool DeleteFlight(FlightContent data)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(), GetPath()))
            {
                var flightData = conn.GetAllWithChildren<FlightDataTable>().
                                    FirstOrDefault(x => x.Key == data.Key);
                if(flightData != null)
                {
                    conn.Delete(flightData);
                    return true;
                }
            }
            return false;
        }

        public static List<Users> ReadUsers()
        {
            var list = new List<Users>();

            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(),GetPath()))
            {
                
                list = conn.Table<Users>().ToList();

            }
            return list;
        }

        public static List<Station> ReadStations()
        {
            var list = new List<Station>();

            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(), GetPath()))
            {
                conn.CreateTable<Station>();
                list = conn.Table<Station>().ToList();

            }
            return list;
        }

        //public static List<T> Read<T>() where T:new()
        //{
        //    var list = new List<T>();

        //    using (var conn = new SQLiteConnection(new SQLitePlatformIOS(),GetPath()))
        //    {
        //        conn.CreateTable<T>();
        //        list = conn.Table<T>().ToList();

        //    }
        //    return list;
        //}

        public static List<Station> GetAllStationsFromUser(string userKey)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(), GetPath()))
            {
                conn.CreateTable<UserStation>();

                var query = conn.Table<UserStation>().FirstOrDefault(x => x.UserKey == userKey);
                if (query == null)
                {
                    return null;
                }
                var station = conn.Table<Station>().Where(x => x.Key == query.StationKey).ToList();
                return station;
            }
        }


        public static Station GetDefaultStation(string userKey)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(),GetPath()))
            {
                conn.CreateTable<UserStation>();
               
                var query = conn.Table<UserStation>().FirstOrDefault(x => x.UserKey == userKey && x.IsDefault);
                if (query == null)
                {
                    return null;
                }
                var station = conn.Table<Station>().FirstOrDefault(x => x.Key == query.StationKey);
                return station;
            }
        }


        public static Station SetDefaultUserStation(string userKey,string stationKey)
        {
            using (var conn = new SQLiteConnection(new SQLitePlatformIOS(),GetPath()))
            {
                conn.CreateTable<UserStation>();
                var query = conn.Table<UserStation>().Where(x => x.UserKey == userKey);
                //conn.CreateTable<Station>();
                var station = conn.Table<Station>().FirstOrDefault(x => x.Key == stationKey);
                foreach (var item in query)
                {
                    if(item.IsDefault && item.StationKey == stationKey)
                    {
                        //var station = conn.Table<Station>().FirstOrDefault(x => x.Key == stationKey);
                        return station;
                    }

                    if(!item.IsDefault && item.StationKey == stationKey)
                    {
                        item.IsDefault = true;
                        conn.Update(item);
                        //var station = conn.Table<Station>().FirstOrDefault(x => x.Key == stationKey);
                        return station;
                    }

                    if(item.IsDefault && item.StationKey != stationKey)
                    {
                        item.IsDefault = false;
                        conn.Update(item);
                        break;
                    }
                        
                }

                var newItem = new UserStation
                {
                    UserKey = userKey,
                    StationKey = stationKey,
                    IsDefault = true
                };
                conn.Insert(newItem);
                return station;
            }
        }

        private static string GetPath()
        {
            var dbName = "grawappDb";
            var folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(folderPath, dbName);
            return dbPath;
        }

        public void GetConnection()
        {
            SQLite.Net.SQLiteConnection db = new SQLite.Net.SQLiteConnection(new SQLitePlatformIOS(), GetPath());
            db.DropTable<InputDataTable>();
            db.CreateTable<InputDataTable>();
            db.CreateTable<FlightDataTable>();
        }
    }
}
