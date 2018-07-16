using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 网易云评论搜索器
{
    public static class Util
    {
        public static DateTime ToDatetime(this long timestamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return startTime.AddMilliseconds(timestamp);
        }
    }
}
