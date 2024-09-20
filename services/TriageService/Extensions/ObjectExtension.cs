using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public static class ObjectExtension
    {
        public static string GetAgeString(this DateTime datetime)
        {
            return DateTime.Now.GetDiffDateTimeString(datetime);
        }

        /// <summary>
        /// 原年龄计算规则
        /// 获取2个日期的差（在当前系统中用于计算年龄）
        /// 年龄显示规则： 
        /// 1、不足24小时显示XX小时或XX小时XX分
        /// 2、不足1月显示XX天
        /// 3、不足一年显示XX月或XX月XX天
        /// 4、超过一年显示XX岁或XX岁XX月
        /// 5、超过6岁，直接显示XX岁
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="datetimeToDiff"></param>
        /// <returns></returns>
        public static string GetDiffDateTimeStringOld(this DateTime datetime, DateTime datetimeToDiff)
        {
            TimeSpan diffTimeSpan = datetime - datetimeToDiff;
            int totalDiffMonths = (datetime.Year - datetimeToDiff.Year) * 12 +
                (datetime.Month - datetimeToDiff.Month) +
                (datetime.Day >= datetimeToDiff.Day ? 0 : -1) /*是否不足1月*/;
            if (diffTimeSpan.TotalHours < 24)
            {// 不足24小时
                var hoursString = $"{diffTimeSpan.Hours}小时";
                var minutesString = diffTimeSpan.Minutes != 0 ? $"{diffTimeSpan.Minutes}分" : "";
                return hoursString + minutesString;
            }
            // 显示岁
            string yearsString = totalDiffMonths < 12u ? "" : $"{totalDiffMonths / 12u}岁";
            // 超过6岁，不显示月份；不足1月，不显示月份
            string monthsString = totalDiffMonths / 12u >= 6 || totalDiffMonths <= 0 ? "" : $"{totalDiffMonths % 12u}月";

            string daysString = "";
            // 不足1月显示XX天
            if (totalDiffMonths < 1u)
            {
                daysString = $"{diffTimeSpan.Days}天";
            }

            return yearsString + monthsString + daysString;
        }

        /// <summary>
        /// 获取2个日期的差（在当前系统中用于计算年龄）
        /// 年龄显示规则： 
        /// 1、最小显示为天，出生日期当天算1天，出生日期第二天算2天，如：昨天18点出生的到今天算2天
        /// 2、不足1月显示XX天
        /// 3、不足一年显示XX月
        /// 4、超过一年显示XX岁xx月
        /// 5、超过6岁，直接显示XX岁
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="datetimeToDiff"></param>
        /// <returns></returns>
        public static string GetDiffDateTimeString(this DateTime datetime, DateTime datetimeToDiff)
        {
            DateTime srcDatetime = datetime.Date.AddDays(1);
            datetimeToDiff = datetimeToDiff.Date;

            TimeSpan diffTimeSpan = srcDatetime - datetimeToDiff;
            int totalDiffMonths = 
                (srcDatetime.Year - datetimeToDiff.Year) * 12 +
                (srcDatetime.Month - datetimeToDiff.Month) +
                (srcDatetime.Day >= datetimeToDiff.Day ? 0 : -1) /*是否不足1月*/;

            // 显示岁
            string yearsString = totalDiffMonths < 12u ? "" : $"{totalDiffMonths / 12u}岁";
            // 超过6岁，不显示月份；不足1月，不显示月份
            string monthsString = totalDiffMonths / 12u >= 6 || totalDiffMonths <= 0 ? "" : $"{totalDiffMonths % 12u}月";

            string daysString = "";
            // 不足1月显示XX天
            if (totalDiffMonths < 1u)
            {
                daysString = $"{diffTimeSpan.Days}天";
            }

            return yearsString + monthsString + daysString;
        }

        /// <summary>
        /// 获取等待时长（xx小时xx分）
        /// </summary>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public static string GetWaitingTimeString(this DateTime beginTime)
        {
            return GetWaitingTimeString(beginTime, DateTime.Now);
        }

        /// <summary>
        /// 获取等待时长（xx小时xx分）
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        private static string GetWaitingTimeString(this DateTime beginTime, DateTime endTime)
        {
            var timespan = endTime.Subtract(beginTime);
            if (timespan.TotalMinutes >= 60)
            {
                return $"{((int)timespan.TotalMinutes) / 60}小时{((int)timespan.TotalMinutes) % 60}分";
            }
            return $"{((int)timespan.TotalMinutes) % 60}分";
        }

        /// <summary>
        /// 判断一个字符串是否包含中文
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns></returns>
        public static bool IsChinese(this string input)
        {
            int chineseBegin = Convert.ToInt32("4e00", 16);
            int chineseEnd = Convert.ToInt32("9fff", 16);
            if (!string.IsNullOrEmpty(input))
            {
                foreach (char letter in input)
                {
                    if (letter >= chineseBegin && letter <= chineseEnd)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
