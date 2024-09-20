using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Responses;

namespace YiJian.Health.Report.PrintSettings;

/// <summary>
/// 打印API
/// </summary>
public interface IPrintSettingAppService : IApplicationService
{
    /// <summary>
    /// 打印设置保存
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<ResponseBase<Guid>> SavePrintSettingAsync(PrintSettingCreateOrUpdate dto);

    /// <summary>
    /// 删除打印设置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ResponseBase<bool>> DeletePrintSettingAsync(Guid id);

    /// <summary>
    /// 根据编码获取
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<ResponseBase<PrintSettingDto>> GetPrintAsync(string code);

    /// <summary>
    /// 获取打印设置列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ResponseBase<List<PrintSettingListDto>>> GetPrintSettingListAsync(PrintSettingInput input);

    /// <summary>
    /// 修改模板状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<ResponseBase<bool>> StatusAsync(Guid id, bool status);

    /// <summary>
    /// 模板是否预览
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isPreview"></param>
    /// <returns></returns>
    Task<ResponseBase<bool>> IsPreviewAsync(Guid id, bool isPreview);
}
