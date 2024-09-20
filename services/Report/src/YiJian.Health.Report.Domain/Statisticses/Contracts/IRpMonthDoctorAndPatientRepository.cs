using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.ReportDatas;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊科医患月度比
    /// </summary>
    public interface IRpMonthDoctorAndPatientRepository : IRepository<StatisticsMonthDoctorAndPatient, int>
    {
        /// <summary>
        /// 查看急诊科医患月度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsMonthDoctorAndPatient>> GetListAsync(DateTime begin, DateTime end);


        /// <summary>
        /// 获取所在时间区间内所有医生的接诊汇总数据
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<UspDoctorPatientRatio>> GetUspDoctorPatientRatiosAsync(DateTime begin, DateTime end);
         
        /// <summary>
        /// 接诊病人详细视图
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<ViewAdmissionRecord>> GetViewAdmissionRecordsAsync(DateTime begin, DateTime end);

        /// <summary>
        /// 调用存储过程采集数据入口
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="veidoo"></param>
        /// <param name="formatDate"></param>
        /// <returns></returns>
        public Task<int> CallUspStatisticsAsync(int reportType, int veidoo, string formatDate);
    }
}
