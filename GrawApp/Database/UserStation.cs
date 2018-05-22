using System;
using SQLite;
using SQLite.Net.Attributes;

namespace GrawApp.Database
{
    public class UserStation
    {
        public UserStation()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserKey { get; set; }
        public string StationKey { get; set; }
        public bool IsDefault { get; set; }
    }
}
