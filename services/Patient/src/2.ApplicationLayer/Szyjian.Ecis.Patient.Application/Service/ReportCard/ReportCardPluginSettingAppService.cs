using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 报卡插件设置
    /// </summary>
    public class ReportCardPluginSettingAppService : EcisPatientAppService, IReportCardPluginSettingAppService
    {
        private readonly IFreeSql _freeSql;
        private readonly ILogger<ReportCardPluginSettingAppService> _log;

        /// <summary>
        /// 报卡插件设置
        /// </summary>
        public ReportCardPluginSettingAppService(IFreeSql freeSql, ILogger<ReportCardPluginSettingAppService> log)
        {
            this._freeSql = freeSql;
            this._log = log;
        }

        /// <summary>
        /// 获取报卡插件设置
        /// </summary>
        /// <param name="isDefault">是否默认配置，需要获得默认配置时,把该值设为True</param>
        public async Task<ResponseResult<ReportCardPluginSettingDto>> GetReportCardPluginSettingAsync(bool isDefault = false, CancellationToken cancellationToken = default)
        {
            try
            {
                var reportCard = await _freeSql.Select<ReportCardPluginSetting>()
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreationTime)
                .ToListAsync();

                // 不存在任何配置记录
                if (reportCard.Count == 0)
                {
                    _log.LogError($"Get ReportCardPluginSetting error. ErrorMsg: 不存在任何配置（包括默认配置）");
                    return RespUtil.Error<ReportCardPluginSettingDto>(msg: "不存在任何配置（包括默认配置）");
                }

                var defaultRecord = reportCard.Where(x => x.IsDefault).FirstOrDefault();
                var record = reportCard.Where(x => !x.IsDefault).FirstOrDefault();

                // 使用Mapster做映射
                ReportCardPluginSettingDto defaultRecordDtos = defaultRecord.BuildAdapter().AdaptToType<ReportCardPluginSettingDto>();
                ReportCardPluginSettingDto recordDtos = record.BuildAdapter().AdaptToType<ReportCardPluginSettingDto>();

                // 获得默认配置
                if (isDefault)
                {
                    if (defaultRecord == null)
                    {
                        _log.LogInformation($"默认配置不存在");
                        return RespUtil.Error<ReportCardPluginSettingDto>(msg: "默认配置不存在");
                    }
                    return RespUtil.Ok(msg: "成功加载默认配置", data: defaultRecordDtos);
                }
                // 当其他配置不存在是，返回默认配置
                else
                {
                    if (record != null)
                    {
                        return RespUtil.Ok(msg: "成功获得配置信息", data: recordDtos);
                    }
                    return RespUtil.Ok(msg: "不存在其他配置，加载默认配置", data: defaultRecordDtos);
                }
            }
            catch (Exception e)
            {
                _log.LogError("Get ReportCardPluginSetting error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<ReportCardPluginSettingDto>(extra: e.Message);
            }
        }

        /// <summary>
        /// 修改报卡插件设置
        /// </summary>
        public async Task<ResponseResult<string>> UpdateReportCardPluginSettingAsync(ReportCardPluginSettingDto dto, CancellationToken cancellationToken)
        {
            try
            {

                // 把原有的非报卡插件设置软删除
                var rows = await _freeSql.Update<ReportCardPluginSetting>()
                        .Set(a => a.IsDeleted, true)
                        .Set(a => a.DeleterId, CurrentUser.Id)
                        .Set(a => a.DeletionTime, DateTime.Now)
                        .Where(x => !x.IsDefault && !x.IsDeleted)
                        .ExecuteAffrowsAsync();
                if (rows > 0)
                {
                    _log.LogInformation($"Soft Delete ReportCardPluginSetting success Affrows = {rows}");
                }

                var record = dto.BuildAdapter().AdaptToType<ReportCardPluginSetting>();
                record.SetID(GuidGenerator.Create());
                record.CreationTime = DateTime.Now;
                record.CreatorId = CurrentUser.Id;
                await _freeSql.Insert(record).ExecuteAffrowsAsync();

                return RespUtil.Ok(data: "success", msg: "保存成功");

            }
            catch (Exception e)
            {
                _log.LogError("Save SaveRelatedDiagnoseList error. ErrorMsg:{Msg}", e);
                return RespUtil.InternalError<string>(extra: e.Message);
            }
        }
    }
}
