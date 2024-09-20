using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Cases.Entities;

namespace YiJian.Cases.Contracts
{
    /// <summary>
    /// 患者病例信息
    /// </summary>
    public interface IPatientCaseRepository : IRepository<PatientCase, int>
    {
        /// <summary>
        /// 获取当前患者最新的病历信息
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        public Task<PatientCase> GetPatientCaseAsync(Guid piid);
    }

}
