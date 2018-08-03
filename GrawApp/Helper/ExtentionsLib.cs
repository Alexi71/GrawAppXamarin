using System;
using System.Globalization;
using Foundation;

namespace GrawApp.Helper
{
    public static class ExtentionsLib
    {
        public static double GetUnixEpoch(this DateTime dateTime)
        {
            var unixTime = dateTime.ToUniversalTime() -
                                   new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixTime.TotalSeconds;
        }

        public static DateTime FromUnixTime(this double unixTime)
        {
            var epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epochTime.AddSeconds(unixTime);
        }

        public static double ToDouble(this string text)
        {
            if(double.TryParse(text, System.Globalization.NumberStyles.Float,CultureInfo.InvariantCulture,out var result))
            {
                return result;
            }
            return double.NaN;
        }

        public static int ToInteger(this string text)
        {
            if (int.TryParse(text, System.Globalization.NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return int.MinValue;
        }

        public static string GetLocalString(this string text)
        {
            return NSBundle.MainBundle.GetLocalizedString(text,text, "");
        }
    }
}
