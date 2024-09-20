using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Domain.Shared.Extension
{
    /// <summary>
    /// 时间扩展类
    /// </summary>
    public static class DateTimeUtility
    {


        /// <summary>
        ///  /********************************************************************************
        ///  ** 描述：扩展日期返回时间戳
        ///  ** 方法名称： ConvertToTimeStamp
        ///  ** 作者： 朱磊
        ///  ** 创建时间：2021-11-04
        ///  ** 最后修改人：（无）
        ///  ** 最后修改时间：（无）
        ///  ** 版权所有 (C) :朱磊
        ///  *********************************************************************************/
        /// </summary>
        /// <param name="Time">当前日期</param>
        /// <returns></returns>
        public static string ConvertToTimeStamp(this DateTime Time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return ((long)(Time.AddHours(-8) - Jan1st1970).TotalMilliseconds).ToString();
        }


        /// <summary>
        /// 时间计算统计扩展
        /// 不足一小时的都算一小时，例如在科2.1小时，算3小时
        /// </summary>
        /// <param name="Date">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="TotalSeconds">总秒数</param>
        /// <returns></returns>
        public static int SecondToHour(this DateTime Date, DateTime EndDate, double TotalSeconds = 0)
        {
            try
            {
                int second = 0;


                if (TotalSeconds > 0)//如果存在秒的数据,优先取用
                {
                    second = Convert.ToInt32(TotalSeconds);
                }
                else
                {
                    //计算差值

                    TimeSpan timeSpan = EndDate - Date;

                    second = Convert.ToInt32(timeSpan.TotalSeconds);

                    if (timeSpan.TotalSeconds < 0)
                    {
                        return 0;
                    }
                }

                int hour = 0, minute = 0;

                if (second > 60)
                {
                    minute = second / 60;
                    second %= 60;
                }
                if (minute > 60)
                {
                    hour = minute / 60;

                    minute %= 60;
                }
                //不足一小时的都算一小时，例如在科2.1小时，算3小时
                if (minute > 0 || second > 0)
                {
                    hour += 1; //小时加1
                }
                return hour;
            }
            catch (Exception)
            {
                return 0;
            }
        }



        /// <summary>
        /// 日期自定义转化扩展 
        /// 20210826
        /// </summary>
        /// <param name="Date"></param>
        /// <param name="Format">转化格式(yyyy-MM-dd)</param>
        /// <returns></returns>
        public static string GetDateFormat(this DateTime Date, string Format)
        {

            return Date.ToString(Format);
        }



        /// <summary>
        /// 本周开始时间
        /// </summary>
        public static DateTime WeekStartTime
        {
            get
            {
                DateTime dt = DateTime.Now.AddDays(-1);  //获取系统当前时间
                DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))); //获取一周的开始日期
                string data = startWeek.ToString("yyyy-MM-dd HH:mm:ss");
                return Convert.ToDateTime(data);
            }
        }


        /// <summary>
        /// 本周结束时间
        /// </summary>
        public static DateTime WeekEndTime
        {
            get
            {
                var EndTime = WeekStartTime.AddDays(6); //获取本周星期天日期

                string data = EndTime.ToString("yyyy-MM-dd HH:mm:ss");

                var Convertdata = Convert.ToDateTime(data);

                return new DateTime(Convertdata.Year, Convertdata.Month, Convertdata.Day, 23, 59, 59);
            }
        }

        public static DateTime WeekStartTime2
        {
            get
            {
                DateTime dt = DateTime.Now;
                int dayOfWeek = -1 * (int)dt.Date.DayOfWeek;
                //Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 4,Friday = 5,Saturday = 6,

                DateTime weekStartTime = dt.AddDays(dayOfWeek + 1);//取本周一
                if (dayOfWeek == 0) //如果今天是周日，则开始时间是上周一
                {
                    weekStartTime = weekStartTime.AddDays(-7);
                }

                return weekStartTime.Date;
            }
        }
    }
}
