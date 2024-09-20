using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Templates.Entities;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Writes.Contracts
{
    /// <summary>
    /// 患者电子病历
    /// </summary>
    public interface IPatientEmrRepository : IRepository<PatientEmr, Guid>
    {
        /// <summary>
        /// 根据就诊流水号，获取关联的病历信息
        /// </summary>
        /// <param name="visitSerialNo"></param>
        /// <returns></returns>
        public Task<List<ViewVisitSerialEmr>> GetEmrByVisitSerialAsync(string visitSerialNo);
    }
}
