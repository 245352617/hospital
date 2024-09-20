using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 疫苗接种
    /// </summary>
    public interface IImmunizationRecordRepository : IRepository<ImmunizationRecord, Guid>
    {
        /// <summary>
        /// 记录一条新的接种记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task AddRecordAsync(ImmunizationRecord model);

        /// <summary>
        /// 获取一条还未提交过的接种记录信息
        /// </summary>
        /// <param name="doctorAdviceId"></param>
        /// <returns></returns>
        public Task<ImmunizationRecord> GetByAdviceIdAsync(Guid doctorAdviceId);

        /// <summary>
        /// 根据患者Id获取患者所有的接种记录
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public Task<IList<ImmunizationRecord>> GetListByPatientIdAsync(string patientId);

        /// <summary>
        /// 更新指定的疫苗接种记录为已提交
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Guid> ConfirmedAsync(Guid id);
    }

}
