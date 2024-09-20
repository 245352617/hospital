using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Health.Report.PrintSettings
{
    /// <summary>
    /// 打印设置
    /// </summary>
    public interface IPrintSettingRepository : IRepository<PrintSetting, Guid>
    {
    }
}