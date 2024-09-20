using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 患者列表设置API
    /// </summary>
    [Authorize]
    public class AdmissionListSettingAppService : EcisPatientAppService, IAdmissionListSettingAppService
    {
        private readonly IFreeSql _freeSql;

        public AdmissionListSettingAppService(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        /// <summary>
        /// 通过表格名称，查询入科列表配置
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<IEnumerable<AdmissionListSettingDto>>> GetAdmissionListSettingAsync(AdmissionListSettingWhereInput input, CancellationToken cancellationToken)
        {
            List<AdmissionListSettingDto> list = await _freeSql.Select<AdmissionListSetting>()
                 .WhereIf(!string.IsNullOrEmpty(input.TableTypeCode), w => w.TableTypeCode == input.TableTypeCode)
                 .OrderBy(o => o.SequenceNo)
                 .ToListAsync<AdmissionListSettingDto>(cancellationToken: cancellationToken);
            return RespUtil.Ok<IEnumerable<AdmissionListSettingDto>>(data: list);
        }

        /// <summary>
        /// 保存入科列表配置集合
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> UpdateAdmissionListSettingAsync(
            List<CreateOrUpdateAdmissionListSettingDto> dto, CancellationToken cancellationToken)
        {
            try
            {
                List<AdmissionListSetting> admissionListSettings = dto.To<List<AdmissionListSetting>>();
                await _freeSql.Update<AdmissionListSetting>().SetSource(admissionListSettings)
                    .IgnoreColumns(a => new
                    {
                        a.DefaultVisible,
                        a.DefaultColumnName,
                        a.DefaultColumnWidth,
                        a.DefaultSequenceNo,
                        a.DefaultShowOverflowTooltip,
                        a.TableTypeCode
                    })
                    .ExecuteAffrowsAsync(cancellationToken);
                return RespUtil.Ok<string>();
            }
            catch (Exception e)
            {
                return RespUtil.Error<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 重置入科列表配置集合
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> ResetAdmissionListSettingAsync(
            AdmissionListSettingWhereInput input, CancellationToken cancellationToken)
        {
            try
            {
                await _freeSql.Update<AdmissionListSetting>()
                    .Set(a => new AdmissionListSetting
                    {
                        ColumnName = a.DefaultColumnName,
                        ColumnWidth = a.DefaultColumnWidth,
                        SequenceNo = a.DefaultSequenceNo,
                        ShowOverflowTooltip = a.DefaultShowOverflowTooltip,
                        Visible = a.DefaultVisible
                    })
                    .Where(a => a.TableTypeCode == input.TableTypeCode)
                    .ExecuteAffrowsAsync(cancellationToken);
                return RespUtil.Ok<string>();
            }
            catch (Exception e)
            {
                return RespUtil.Error<string>(msg: e.Message);
            }
        }
    }
}