using System;
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
    }
}
