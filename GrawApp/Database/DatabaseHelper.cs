using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GrawApp.Database
{
    public class DatabaseHelper
    {
        public DatabaseHelper()
        {
        }

        public static bool Insert<T>(ref T data)
        {
            using(var conn = new SQLite.SQLiteConnection(GetPath()))
            {
                conn.CreateTable<T>();
                if(conn.Insert(data) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<Users> ReadUsers()
        {
            var list = new List<Users>();

            using (var conn = new SQLite.SQLiteConnection(GetPath()))
            {
                list = conn.Table<Users>().ToList();

            }
            return list;
        }

        public static List<T> Read<T>() where T:new()
        {
            var list = new List<T>();

            using (var conn = new SQLite.SQLiteConnection(GetPath()))
            {
                list = conn.Table<T>().ToList();

            }
            return list;
        }

        private static string GetPath()
        {
            var dbName = "grawappDb";
            var folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(folderPath, dbName);
            return dbPath;
        }
    }
}
