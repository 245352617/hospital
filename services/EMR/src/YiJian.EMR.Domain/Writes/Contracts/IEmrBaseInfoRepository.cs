using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Writes.Contracts
{
    /// <summary>
    /// 电子病例采集的基础信息
    /// </summary>
    public interface IEmrBaseInfoRepository : IRepository<EmrBaseInfo, Guid>
    {

        /// <summary>
        /// 根据挂号识别号获取采集过来的患者的电子病例基础信息
        /// </summary>
        /// <param name="registerNo"></param>
        /// <returns></returns> 
        public Task<EmrBaseInfo> GetByAsync(string registerNo);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Guid> AddAsync(EmrBaseInfo model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Guid> ModifyAsync(EmrBaseInfo model);
    }
}
