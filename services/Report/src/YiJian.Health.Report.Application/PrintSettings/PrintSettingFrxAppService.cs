using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.Health.Report.PrintSettings;

/// <summary>
/// 打印设置API
/// </summary>
[Authorize]
public class PrintSettingFrxAppService : ReportAppService, IPrintSettingFrxAppService
{
    private readonly IPrintSettingRepository _printSettingRepository;

    /// <summary>
    /// 打印设置API
    /// </summary>
    /// <param name="printSettingRepository"></param>
    public PrintSettingFrxAppService(IPrintSettingRepository printSettingRepository)
    {
        _printSettingRepository = printSettingRepository;
    }

    /// <summary>
    /// 下载Frx模板
    /// </summary>
    /// <param name="id">模板Id</param>
    /// <returns></returns>
    public async Task<IActionResult> GetDownloadFrxAsync(Guid id)
    {
        var entity = await (await _printSettingRepository.GetQueryableAsync())
            .Select(s => new PrintSettingFrxDto
            {
                Id = s.Id,
                Name = s.Name,
                TempContent = s.TempContent
            }).FirstOrDefaultAsync(w => w.Id == id);
        var filename = $"{entity.Name}_{entity.Id.ToString("N")}.frx";
        MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(entity.TempContent));
        string mimeType = "text/plain";
        return new FileStreamResult(stream, mimeType)
        {
            FileDownloadName = filename
        };
    }

}