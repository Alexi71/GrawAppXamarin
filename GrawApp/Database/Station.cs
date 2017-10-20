using System;
using SQLite;

namespace GrawApp.Database
{
    public class Station
    {
        public Station()
        {
        }
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Altitude { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string StationId { get; set; }
        public string Key { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Name { get; set; }
    }
}
