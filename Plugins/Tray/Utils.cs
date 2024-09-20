using System;
using System.Globalization;

namespace YiJian.CardReader
{
    internal class Utils
    {
        public static DateTime? SetDateTime(string value)
        {
            try
            {
                IFormatProvider ifp = new CultureInfo("zh-CN", true);
                DateTime time = DateTime.ParseExact(value, "yyyyMMdd", ifp);
                return time;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
