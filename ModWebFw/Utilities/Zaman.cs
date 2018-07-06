using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModWebFw
{
    public static class Zaman
    {
        public static DateTime Simdi
        {
            get
            {
                var info = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
                DateTimeOffset localServerTime = DateTimeOffset.Now;
                return TimeZoneInfo.ConvertTime(localServerTime, info).DateTime;
                //return DateTime.Now;
            }
        }
    }
}
