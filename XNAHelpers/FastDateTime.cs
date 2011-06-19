using System;

namespace XNAHelpers
{
    public static class FastDateTime
    {
        public static TimeSpan LocalUtcOffset { get; private set; }

        public static DateTime Now
        {
            get { return ToLocalTime(DateTime.UtcNow); }
        }

        static FastDateTime()
        {
            LocalUtcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
        }

        public static DateTime ToLocalTime(DateTime dateTime)
        {
            return dateTime + LocalUtcOffset;
        }
    }
}