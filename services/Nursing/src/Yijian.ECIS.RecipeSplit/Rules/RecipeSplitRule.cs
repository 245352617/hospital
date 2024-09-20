using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yijian.ECIS.RecipeSplit.Contracts;

namespace Yijian.ECIS.RecipeSplit
{
    /// <summary>
    /// 药品批次规则引擎
    /// </summary>
    internal partial class RecipeSplitRule
    {
        /// <summary>
        /// 药品频次拆顿
        /// </summary>
        /// <param name="frequency">药品频次信息</param>
        /// <param name="firstDay">开始日期，一般是开始时间或者是开嘱日期</param>
        /// <returns>拆顿后的完整时间列表</returns>
        /// <exception cref="ArgumentException">错误的参数信息</exception>
        public IEnumerable<DateTime> Split(Frequency frequency, DateTime firstDay)
        {
            if (frequency.FrequencyCode == null) throw new ArgumentException($"FrequencyCode不能为null！");
            if (frequency.StartTime == null) throw new ArgumentNullException($"开始时间不能为空！");
            if (frequency.EndTime == null) throw new ArgumentNullException($"结束时间不能为空！");

            var startTime = frequency.StartTime.Value;  //外部已经处理，必然不为空
            var endTime = frequency.EndTime.Value;      //外部已经处理，必然不为空

            var rule = FrequencyUnitAnalysis.UnitAnalysis(frequency);
            var defaultweek = firstDay.DayOfWeek;

            return rule.UnitType switch
            {
                FrequencyUnitType.ST => new[] { startTime },
                FrequencyUnitType.Hour => UnitTypeHour(startTime, endTime, rule),
                FrequencyUnitType.NHour => UnitTypeHour(startTime, endTime, rule),
                FrequencyUnitType.Daily => UnitTypeDaily(firstDay, startTime, endTime, rule),
                FrequencyUnitType.NDay => UnitTypeDaily(firstDay, startTime, endTime, rule),
                FrequencyUnitType.Weekly => UnitTypeWeekly(frequency, startTime, endTime, rule, defaultweek),
                FrequencyUnitType.NWeek => UnitTypeWeekly(frequency, startTime, endTime, rule, defaultweek),
                _ => Enumerable.Empty<DateTime>()
            };

            //每小时，几小时
            static IEnumerable<DateTime> UnitTypeHour(DateTime startTime, DateTime endTime, FrequencyRule rule)
            {
                var startDay = startTime.Date;
                var endDay = endTime.Date;
                var timeList = new List<DateTime>();
                var lastetTime = startDay;
                var times = rule.Frequency.ExecuteDayTime.SplitDayTimeArray().Select(p => p.ToTimeSpan()).ToList();
                do
                {
                    var lastDay = new DateTime(lastetTime.Year, lastetTime.Month, lastetTime.Day, lastetTime.Hour, 0, 0);
                    lastDay = lastDay.AddHours(rule.UnitOffset);
                    foreach (var time in times)
                    {
                        lastetTime = lastDay.Add(time);
                        timeList.Add(lastetTime);
                    }
                } while (lastetTime.Date <= endDay);
                var list = timeList.Where(p => p >= startTime && p <= endTime).ToList();
                list.Sort();
                return list;
            }

            //每天，隔天
            static IEnumerable<DateTime> UnitTypeDaily(DateTime firstDay, DateTime startTime, DateTime endTime, FrequencyRule rule)
            {
                var startDay = firstDay.Date;
                var endDay = endTime.Date;
                //var dayList = new List<DateTime>();
                //var day = startDay;
                //do
                //{
                //    dayList.Add(day);
                //    day = day.AddDays(rule.UnitOffset);

                //} while (day <= endDay);
                var daytimeArray = rule.Frequency.ExecuteDayTime.SplitDayTimeArray();
                var datetimesInOneDay = daytimeArray.Select(p => p.ToTimeSpan()).ToList();
                return GenDailyTimeQueue(startTime, endTime, rule.UnitOffset, datetimesInOneDay);
            }

            //每周，隔周
            static IEnumerable<DateTime> UnitTypeWeekly(Frequency frequency, DateTime startTime, DateTime endTime, FrequencyRule rule, DayOfWeek defaultweek)
            {
                var arrTimeStr = frequency.PreparedDayTime.SplitWeekTimeArray();
                if (arrTimeStr.Length == 0)
                {
                    arrTimeStr = frequency.PreparedDayTime.SplitDayTimeArray();
                }
                var datetimesInOneWeek = new List<DateTime>();
                foreach (var timeString in arrTimeStr.AsEnumerable())
                {
                    var timespan = timeString.ToWeekTimeSpan(defaultweek, out DayOfWeek week);
                    var firstWeekDay = startTime.NextWeekDay(week);
                    var datetime = firstWeekDay.Add(timespan);
                    datetimesInOneWeek.Add(datetime);
                }
                datetimesInOneWeek.Sort();
                return GenWeeklyTimeQueue(startTime, endTime, rule.UnitOffset, datetimesInOneWeek);
            }

            //通过第一周的时间点，推断后续的时间点
            static List<DateTime> GenWeeklyTimeQueue(DateTime startTime, DateTime endTime, int unitOffset, List<DateTime> datetimesInOneWeek)
            {
                var dtList = new List<DateTime>();
                int dayOffset = 0;
                int i = 0, max = 10000; //防止死循环
                do
                {
                    foreach (var oneTimeInOneWeek in datetimesInOneWeek)
                    {
                        var datetime = oneTimeInOneWeek.AddDays(dayOffset); //将起始一周内的各天分别增加周期性的天数
                        if (datetime >= endTime)
                        {
                            return dtList;
                        }
                        if (datetime >= startTime)
                        {
                            dtList.Add(datetime);
                        }
                        if (i++ > max)  //防止死循环
                        {
                            return dtList;
                        }
                    }
                    dayOffset += unitOffset * 7;    //以周为周期递增
                } while (true);
            }

            //通过第一天的时间点，推断后续的时间点
            static List<DateTime> GenDailyTimeQueue(DateTime startTime, DateTime endTime, int unitOffset, List<TimeSpan> datetimesInOneDay)
            {
                var dtList = new List<DateTime>();
                int i = 0, max = 10000; //防止死循环
                var date = startTime.Date;
                do
                {
                    foreach (var oneTimeInOneDay in datetimesInOneDay)
                    {
                        var datetime = date.Add(oneTimeInOneDay);   //将周期性的天增加一天内的各时间点
                        if (datetime >= endTime)
                        {
                            return dtList;
                        }
                        if (datetime >= startTime)
                        {
                            dtList.Add(datetime);
                        }
                        if (i++ > max)  //防止死循环
                        {
                            return dtList;
                        }
                    }
                    date = date.AddDays(unitOffset);    //以天为周期递增
                } while (true);
            }
        }


    }
}
