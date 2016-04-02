using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Photogram.WebApp.Models
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Converts a DateTime to a javascript timestamp.
        /// http://stackoverflow.com/a/5117291/13932
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The javascript timestamp.</returns>
        public static long ToJavascriptTimestamp(this DateTime input)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time = input.Subtract(new TimeSpan(epoch.Ticks));
            return time.Ticks / 10000;
        }
    }
}