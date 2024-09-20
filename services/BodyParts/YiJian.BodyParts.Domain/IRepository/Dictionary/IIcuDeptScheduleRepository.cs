using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:科室班次
    /// </summary>
    public interface IIcuDeptScheduleRepository : IRepository<IcuDeptSchedule, Guid>, IBaseRepository<IcuDeptSchedule, Guid>
    {
        /// <summary>
        /// 根据患者id获取当天班次日期(YYYY-MM-DD)
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <returns></returns>
        Task<DateTime> CurrentTime(string PI_ID, string deptCode);

        /// <summary>
        /// 根据科室代码获取当天班次日期(YYYY-MM-DD)
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        Task<DateTime> GetCurrentTime(string deptCode);

        /// <summary>
        /// 根据班次日期计算自然日时间范围
        /// </summary>
        /// <param name="PI_ID">患者id</param>
        /// <param name="ScheduleCode">班次代码</param>
        /// <param name="ScheduleTime">班次日期(YYYY-MM-DD)</param>
        /// <returns></returns>
        Task<List<DateTime>> ScheduleTimes(string PI_ID, string ScheduleCode, DateTime ScheduleTime);

        /// <summary>
        /// 根据班次日期计算自然日时间范围
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="ScheduleCode">班次代码</param>
        /// <param name="ScheduleTime">班次日期(YYYY-MM-DD)</param>
        /// <returns></returns>
        Task<List<DateTime>> GetScheduleTimes(string deptCode, string ScheduleCode, DateTime ScheduleTime);


        /// <summary>
        /// 根据科室代码、护理时间得到班次时间范围
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="ScheduleTime">班次日期</param>
        /// <returns></returns>
        Task<List<DateTime>> GetScheduleTimesByNurseTime(string deptCode, DateTime nurseTime);

        /// <summary>
        /// 根据科室代码，护理时间获取班次代码
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="nurseTime"></param>
        /// <returns></returns>
        string GetScheduleCode(string deptCode, DateTime nurseTime);

        /// <summary>
        /// 根据科室代码获取上一班次信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        LastScheduleDto GetLastSchedule(string deptCode);

        /// <summary>
        /// 获取班次
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        Task<List<IcuDeptSchedule>> GetIcuDeptSchedule(string deptCode);

        /// <summary>
        /// 获取班次代码和班次时间列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="nurseDate"></param>
        /// <returns></returns>
        Task<List<ScheduleCodeAndTimeDto>> GetScheduleCodeAndTimes(string deptCode, string nurseDate);

        /// <summary>
        /// 查询指定时间对应的班次日期（yyyy-MM-dd）
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="nurseTime"></param>
        /// <returns></returns>
        Task<DateTime> GetScheduleDate(string deptCode,DateTime nurseTime);

        /// <summary>
        /// 查询当天所有班次对应时间
        /// 班次结束时间是不包含的
        /// 如果要作为条件使用，应该是 nurseTime>=dic[scheduleName][0] && nurseTime < dic[scheduleName][dic[scheduleName].Count-1]
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="nurseTime"></param>
        /// <returns>班次中的最后一个时间是闭区间（不包含最后一个时间点）</returns>
        Task<Dictionary<string,List<DateTime>>> GetScheduleTimesWithNameAsync(string deptCode,DateTime nurseTime);

        /// <summary>
        /// 通过护理时间列表查询所有时间对应班次日期
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="nurseTimes"></param>
        /// <returns></returns>
        Task<List<DateTime>> GetScheduleTimesByNurseTimes(string deptCode, List<DateTime> nurseTimes);

        /// <summary>
        /// 获取班次的类型
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns>1前闭后开 2前开后闭</returns>
        Task<int> GetScheduleTimeType(string deptCode);
    }
}
