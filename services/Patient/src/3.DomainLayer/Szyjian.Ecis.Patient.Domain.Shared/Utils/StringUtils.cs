using System.Text;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public class StringUtils
    {
        public static string ConverterTime(double totalSeconds, bool isIncludeDay = false, bool IsNeedSeconds = false)
        {
            StringBuilder sb = new StringBuilder();
            if (isIncludeDay)
            {
                int day = (int)(totalSeconds / (24 * 60 * 60));
                if (day > 0)
                {
                    sb.Append(day + "天");
                }
                totalSeconds = totalSeconds % (24 * 60 * 60);
            }

            int hour = (int)(totalSeconds / (60 * 60));
            if (hour > 0)
            {
                sb.Append(hour + "时");
            }
            totalSeconds = totalSeconds % (60 * 60);

            int minute = (int)(totalSeconds / 60);
            if (minute > 0)
            {
                sb.Append(minute + "分");
            }

            if (IsNeedSeconds)
            {
                int second = (int)(totalSeconds % 60);
                sb.Append(second + "秒");
            }

            return sb.ToString();
        }

    }
}
