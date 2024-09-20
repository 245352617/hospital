using System;
using System.Collections.Generic;
using System.Text;

namespace Yijian.ECIS.RecipeSplit.Contracts
{
    public interface IRecipeSplit
    {
        ///// <summary>
        ///// 药品频次名称对照映射配置
        ///// </summary>
        ///// <param name="map"></param>
        //public void FrequencyMapper(Dictionary<string, string> map);

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
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<DateTime> Split(Frequency frequency, DateTime conversionTime, bool isLongAdvice, int dayOffset, TimeSpan startDayTime, TimeSpan endDayTime, bool isFirstSplit = false);


    }
}
