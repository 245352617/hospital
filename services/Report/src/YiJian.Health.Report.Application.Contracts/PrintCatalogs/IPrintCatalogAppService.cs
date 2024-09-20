using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.PrintSettings;

namespace YiJian.Health.Report.PrintCatalogs
{
    /// <summary>
    /// 打印目录
    /// </summary>
    public interface IPrintCatalogAppService : IApplicationService
    {
        /// <summary>
        /// 保存打印目录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ResponseBase<Guid>> SavePrintCatalogAsync(PrintCatalogDto dto);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseBase<bool>> DeletePrintCatalogAsync(Guid id);

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        Task<ResponseBase<List<PrintCatalogDto>>> GetListAsync();

        /// <summary>
        /// 打印中心查询打印
        /// </summary>
        /// <param name="piid"></param>
        /// <param name="platformType"></param>
        /// <param name="isPrint"></param>
        /// <returns></returns>
        Task<ResponseBase<List<PrintSettingListDto>>> GetPrintCenterListAsync(Guid piid,
            EPlatformType platformType = EPlatformType.EmergencyTreatment,int isPrint=-1);
    }
}