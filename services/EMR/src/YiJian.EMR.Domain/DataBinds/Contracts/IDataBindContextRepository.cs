using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.DataBinds.Entities;

namespace YiJian.EMR.DataBinds.Contracts
{
    /// <summary>
    /// 数据绑定的上下文
    /// </summary>
    public interface IDataBindContextRepository : IRepository<DataBindContext, Guid>
    {
        /// <summary>
        /// 获取数据绑定的上下文内容
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public Task<DataBindContext> GetDetailAsync(Guid patientEmrId);
    }
}
