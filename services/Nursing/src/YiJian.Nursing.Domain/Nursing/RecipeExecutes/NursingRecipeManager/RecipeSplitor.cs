using System;
using System.Collections.Generic;
using System.Linq;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 医嘱拆顿执行器
    /// </summary>
    internal class RecipeSplitor
    {
        private static readonly Lazy<RecipeSplitRule> recipeSplitRule = new Lazy<RecipeSplitRule>(() => new RecipeSplitRule());
        /// <summary>
        /// 医嘱拆顿
        /// </summary>
        /// <param name="frequency">频次信息</param>
        /// <param name="conversionTime">拆顿时间</param>
        /// <param name="isLongAdvice">是否长嘱</param>
        /// <param name="dayOffset">提前几天进行医嘱拆顿</param>
        /// <param name="startDayTime">要被拆顿的医嘱执行日期对应的开始时间偏移量</param>
        /// <param name="endDayTime">要被拆顿的医嘱执行日期对应的结束时间偏移量</param>
        /// <param name="isFirstSplit">是否首日拆顿。开嘱的时候会进行一次拆顿，就是首日拆顿，后续定时任务每日拆顿，就是非首日拆顿。</param>
        /// <param name="prescribeDay">临嘱天数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<DateTime> Split(Frequency frequency, DateTime conversionTime, bool isLongAdvice, int dayOffset, TimeSpan startDayTime, TimeSpan endDayTime, bool isFirstSplit = false, int prescribeDay = 1)
        {
            if (string.IsNullOrWhiteSpace(frequency.FrequencyCode))
            {
                return Enumerable.Empty<DateTime>();
            }

            var firstDay = frequency.StartTime.GetValueOrDefault().Date;

            DateTime startTime;
            DateTime endTime;
            if (isLongAdvice)  //长嘱
            {
                startTime = isFirstSplit ?
                    conversionTime                                                      //首日拆顿，直接使用拆顿时间
                    : conversionTime.Date.AddDays(dayOffset).Add(startDayTime);         //拆顿当天和要拆的执行日期的偏移量

                bool isNextDay = conversionTime.TimeOfDay < startDayTime;               //拆顿时间过了凌晨
                endTime = (isFirstSplit && isNextDay)
                    ? conversionTime.Date.AddDays(-1).Add(endDayTime)                   //首日拆顿，拆顿时间过了凌晨
                    : startTime.Date.Add(endDayTime);                                   //否则，使用开始时间那天的下班时间
                if (frequency.EndTime.HasValue && frequency.EndTime.Value < endTime)    //如果填写了结束时间et1，并且填写的结束时间et1比计算得到的默认结束时间et2早，则使用填写的结束时间et1。
                {
                    endTime = frequency.EndTime.Value;
                }
            }
            else  //不是长嘱的都按临嘱算
            {
                startTime = conversionTime;     //临嘱默认开始时间=当前时间
                endTime = startTime.Date.AddDays(prescribeDay); //临嘱默认结束时间=当前时间加24小时。
            }
            frequency.StartTime = startTime;
            frequency.EndTime = endTime;
            return recipeSplitRule.Value.Split(frequency, firstDay);
        }

    }
}
