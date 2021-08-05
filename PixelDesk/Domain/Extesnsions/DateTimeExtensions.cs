using System;

namespace PixelDesk.Domain.Extesnsions
{
    public static class DateTimeExtensions
    {
        public static DateTime Local(this DateTime dateTime)
        {
            var timeZone = Environment.GetEnvironmentVariable("TIMEZONE");

            if (string.IsNullOrEmpty(timeZone)) timeZone = "0";

            return dateTime.AddHours(int.Parse(timeZone));
        }
    }
}