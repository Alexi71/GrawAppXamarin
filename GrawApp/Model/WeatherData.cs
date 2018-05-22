using System;
namespace GrawApp.Model
{
    public class WeatherData
    {
        public double Temperature { get; set; }
        public int Condition { get; set; }
        public string City { get; set; }
        public string IconName { get; set; }

        public WeatherData()
        {
        }

        public string GetWeatherIcon(int condition)
        {
            switch (condition)
            {
                case int n when (n>=0 && n< 300):
                    return "tstorm1";

                case int n when (n>=301 && n<500):
                    return "light_rain";
                case int n when (n>501 && n < 600):
                    return "shower3";
                case int n when (n > 601 && n < 700):
                    return "snow4";
                case int n when (n > 701 && n < 771):
                    return "fog";        
                case int n when (n > 772 && n < 799):
                    return "tstorm3";    
                case 800:
                    return "sunny";
                case int n when (n >= 801 && n <= 804):
                    return "cloudy2";
                case int n when ((n > 900 && n < 903)||(n >=905 && n <= 1000)):
                    return "tstorm3";
                case 903:
                    return "snow5";
                case 904:
                    return "sunny";

                default:
                    return "dunno";
            }
        }
    }
}
