using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingSettings.Entities;

namespace YiJian.Health.Report.NursingSettings.Contracts
{
    /// <summary>
    /// 护理单表头配置
    /// </summary>
    public interface INursingSettingHeaderRepository : IRepository<NursingSettingHeader, Guid>
    {
        /// <summary>
        /// 获取动态六项表单域内容集合
        /// </summary>
        /// <param name="headerId">表头描述 如:气道,循环,出血</param>
        /// <returns></returns>
        public Task<NursingSettingHeader> GetSixInputOptionsAsync(Guid headerId);
    }
}
