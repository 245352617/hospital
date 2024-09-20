using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingSettings.Entities;

namespace YiJian.Health.Report.NursingSettings.Contracts
{
    /// <summary>
    /// 护理单配置
    /// </summary>
    public interface INursingSettingRepository : IRepository<NursingSetting, Guid>
    {
        /// <summary>
        /// 获取所有的护理单主题集合
        /// </summary>
        /// <returns></returns>
        public Task<List<NursingSetting>> GetAllNursingSheetListAsync();

        /// <summary>
        /// 根据分组ID获取护理单主题集合
        /// </summary>
        /// <returns></returns>
        public Task<List<NursingSetting>> GetNursingSheetListAsync(string groupId);

        /// <summary>
        /// 获取完整的配置项集合
        /// </summary> 
        /// <param name="isDynamicSix"></param>
        /// <param name="headers"></param>
        /// <param name="notinheaders"></param> 
        /// <returns></returns>
        public Task<List<NursingSetting>> GetAllSettingsAsync(bool isDynamicSix, List<Guid> headers, List<Guid> notinheaders);

    }
}
