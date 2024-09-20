using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yijian.ECIS.RecipeSplit
{
    /// <summary>
    /// 字符串拆分扩展类
    /// </summary>
    internal static class SplitExtention
    {
        /// <summary>
        /// 以分号分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitWeekTimeArray(this string str)
        {
            if (!str.Contains(';') && !str.Contains('；'))
            {
                return new string[0];
            }
            return str.Split(new char[] { ';', '；' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 以逗号减号分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitDayTimeArray(this string str)
        {
            return str.Split(new char[] { ',', '，', '-' }, StringSplitOptions.RemoveEmptyEntries);
        }

        ///// <summary>
        ///// 冒号分割字符串
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string[] SplitWithColon(this string str)
        //{
        //    return str.Split(new char[] { ':', '：' }, StringSplitOptions.RemoveEmptyEntries);
        //}

        ///// <summary>
        ///// 冒号和点分割字符串，形如："21:00:00.000"
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string[] SplitWithColonPoint(this string str)
        //{
        //    return str.Split(new char[] { ':', '：', '.', '。' }, StringSplitOptions.RemoveEmptyEntries);
        //}

        /// <summary>
        /// 12
        /// </summary>
        private static readonly Regex regexHH = new Regex(@"^(20|21|22|23|\d|[0-1]?\d)$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        /// <summary>
        /// 12:00
        /// </summary>
        private static readonly Regex regexHHmm = new Regex(@"^(20|21|22|23|[0-1]?\d)[:：]([0-5]?\d)$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        /// <summary>
        /// 12:00:00
        /// </summary>
        private static readonly Regex regexHHmmss = new Regex(@"^(20|21|22|23|[0-1]?\d)[:：]([0-5]?\d)[:：]([0-5]?\d)$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        /// <summary>
        /// 12:00:00.000
        /// </summary>
        private static readonly Regex regexHHmmssfff = new Regex(@"^(20|21|22|23|[0-1]?\d)[:：]([0-5]?\d)[:：]([0-5]?\d)[\.]0{1,3}$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        /// <summary>
        /// 转换
        /// 12
        /// 12:00
        /// 12:00:00
        /// 12:00:00.000
        /// 这类格式的时间到TimeSpan
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static TimeSpan ToTimeSpan(this string str)
        {
            var matchHHmmssfff = regexHHmmssfff.Match(str);
            if (matchHHmmssfff.Success)
            {
                var v = matchHHmmssfff.Groups.ToArray()[1..];
                return new TimeSpan(int.Parse(v[0].Value), int.Parse(v[1].Value), int.Parse(v[2].Value));
            }

            var matchHHmmss = regexHHmmss.Match(str);
            if (matchHHmmss.Success)
            {
                var v = matchHHmmss.Groups.ToArray()[1..];
                return new TimeSpan(int.Parse(v[0].Value), int.Parse(v[1].Value), int.Parse(v[2].Value));
            }

            var matchHHmm = regexHHmm.Match(str);
            if (matchHHmm.Success)
            {
                var v = matchHHmm.Groups.ToArray()[1..];
                return new TimeSpan(int.Parse(v[0].Value), int.Parse(v[1].Value), 0);
            }
            var matchHH = regexHH.Match(str);
            if (matchHH.Success)
            {
                var v = matchHH.Groups.ToArray()[1..];
                return new TimeSpan(int.Parse(v[0].Value), 0, 0);
            }

            throw new Exception($"未匹配的执行时间格式！str：{str}");
        }

        /// <summary>
        /// 周四:08
        /// </summary>
        private static readonly Regex regexWeekTimeWWHH = new Regex(@"^(周[一二三四五六七天日])[:：](20|21|22|23|[0-1]?\d)$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        /// <summary>
        /// 周四:08:00
        /// </summary>
        private static readonly Regex regexWeekTimeWWHHmm = new Regex(@"^(周[一二三四五六七天日])[:：](20|21|22|23|[0-1]?\d)[:：]([0-5]?\d)$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
        /// <summary>
        /// 转换周四:08:00这类格式的周时间到TimeSpan和周的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static TimeSpan ToWeekTimeSpan(this string str, DayOfWeek defaultWeek, out DayOfWeek week)
        {
            var matchWWHHmm = regexWeekTimeWWHHmm.Match(str);
            if (matchWWHHmm.Success)
            {
                var v = matchWWHHmm.Groups.ToArray()[1..];
                week = v[0].Value.ToDayOfWeek();
                return new TimeSpan(int.Parse(v[1].Value), int.Parse(v[2].Value), 0);
            }

            var matchWWHH = regexWeekTimeWWHH.Match(str);
            if (matchWWHH.Success)
            {
                var v = matchWWHH.Groups.ToArray()[1..];
                week = v[0].Value.ToDayOfWeek();
                return new TimeSpan(int.Parse(v[1].Value), int.Parse(v[2].Value), 0);
            }

            week = defaultWeek;
            return str.ToTimeSpan();
        }


        /// <summary>
        /// 将中文的周几转换成周的枚举
        /// </summary>
        /// <param name="week">形如"周一"、"周日"、"周天"的字符串</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static DayOfWeek ToDayOfWeek(this string week)
        {
            switch (week)
            {
                case "周日":
                case "周天":
                    return DayOfWeek.Sunday;
                case "周一":
                    return DayOfWeek.Monday;
                case "周二":
                    return DayOfWeek.Tuesday;
                case "周三":
                    return DayOfWeek.Wednesday;
                case "周四":
                    return DayOfWeek.Thursday;
                case "周五":
                    return DayOfWeek.Friday;
                case "周六":
                    return DayOfWeek.Saturday;
                default:
                    throw new ArgumentOutOfRangeException(nameof(week), $"week:{week}");
            }
        }

        /// <summary>
        /// 基于当前日期计算这天及之后第一个周几的日期
        /// </summary>
        /// <param name="ts">当前日期</param>
        /// <param name="nextDayOfWeek">这天及之后第一个周几的日期</param>
        /// <returns></returns>
        public static DateTime NextWeekDay(this DateTime ts, DayOfWeek nextDayOfWeek)
        {
            var offset = ((int)nextDayOfWeek + 7 - (int)ts.DayOfWeek) % 7;
            return ts.AddDays(offset);
        }


    }
}
