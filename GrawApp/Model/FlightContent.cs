using System;
namespace GrawApp.Model
{
    public class FlightContent
    {
        public string Key { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string Url100 { get; set; }
        public string UrlEnd { get; set; }
        public bool IsRealTimeFlight { get; set; }
        public double EpochTime { get; set; }
       

        public FlightContent()
        {
        }
    }
}
