using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    /// 床位设置API
    /// </summary>
    [Authorize]
    public class BedAppService : EcisPatientAppService, IBedAppService
    {
        private readonly IFreeSql _freeSql;

        public BedAppService(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        /// <summary>
        /// 获取病床列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<IEnumerable<BedDto>>> GetBedListAsync(BedWhereInput input, CancellationToken cancellationToken)
        {
            try
            {
                List<BedDto> list = await _freeSql.Select<Bed>()
                     .WhereIf(!string.IsNullOrEmpty(input.BedAreaCode), w => w.BedAreaCode == input.BedAreaCode)
                     .WhereIf(input.IsShow.HasValue, x => x.IsShow == input.IsShow)
                     .ToListAsync<BedDto>(cancellationToken);
                return RespUtil.Ok<IEnumerable<BedDto>>(data: list);
            }
            catch (Exception e)
            {
                return RespUtil.Error<IEnumerable<BedDto>>(msg: e.Message);
            }
        }


        /// <summary>
        /// 新增或修改床位
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("/api/patientService/bed/CreateOrUpdateBed")]
        public async Task<ResponseResult<string>> CreateOrUpdateBedAsync(CreateOrUpdateBedDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                if (dto.Id == -1)
                {
                    Bed bed = dto.To<Bed>();
                    if (_freeSql.Select<Bed>().Any(x => x.BedName == bed.BedName))
                    {
                        return RespUtil.Error<string>(msg: "已经存在相同名称的床位");
                    }

                    await _freeSql.Insert(bed).ExecuteAffrowsAsync(cancellationToken);
                }
                else
                {
                    Bed bed = await _freeSql.Select<Bed>().Where(w => w.Id == dto.Id).FirstAsync<Bed>(cancellationToken);
                    if (bed == null)
                    {
                        return RespUtil.Error<string>(msg: "数据不存在");
                    }

                    if (await _freeSql.Select<AdmissionRecord>().AnyAsync(a => a.Bed == bed.BedName && a.VisitStatus == VisitStatus.正在就诊 && a.AreaCode == bed.BedAreaCode, cancellationToken))
                    {
                        return RespUtil.Error<string>(msg: "床位使用中，无法编辑");
                    }

                    if (_freeSql.Select<Bed>().Any(x => x.BedName == dto.BedName && x.Id != dto.Id))
                    {
                        return RespUtil.Error<string>(msg: "已经存在相同名称的床位");
                    }

                    await _freeSql.Update<Bed>().IgnoreColumns(i => i.Id).SetDto(dto).Where(w => w.Id == dto.Id).ExecuteAffrowsAsync(cancellationToken);
                }

                return RespUtil.Ok<string>();
            }
            catch (Exception e)
            {
                return RespUtil.Error<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 删除床位
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> DeleteBedAsync(BedWhereInput input,
            CancellationToken cancellationToken)
        {
            try
            {
                Bed bed = await _freeSql.Select<Bed>().Where(w => w.Id == input.Id).FirstAsync(cancellationToken);
                if (bed == null)
                {
                    return RespUtil.Error<string>(msg: "数据不存在");
                }

                if (await _freeSql.Select<AdmissionRecord>().AnyAsync(a => a.Bed == bed.BedName && a.VisitStatus == VisitStatus.正在就诊 && a.AreaCode == bed.BedAreaCode, cancellationToken))
                {
                    return RespUtil.Error<string>(msg: "床位使用中，无法删除");
                }

                await _freeSql.Delete<Bed>().Where(w => w.Id == input.Id).ExecuteAffrowsAsync(cancellationToken);
                return RespUtil.Ok<string>();
            }
            catch (Exception e)
            {
                return RespUtil.Error<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 根据id获取床位信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<BedDto>> GetBedAsync(BedWhereInput input,
            CancellationToken cancellationToken)
        {
            try
            {
                BedDto bedDto = await _freeSql.Select<Bed>().Where(w => w.Id == input.Id)
                    .FirstAsync<BedDto>(cancellationToken);
                if (bedDto == null)
                {
                    return RespUtil.Error<BedDto>(msg: "数据不存在");
                }

                return RespUtil.Ok(data: bedDto);
            }
            catch (Exception e)
            {
                return RespUtil.Error<BedDto>(msg: e.Message);
            }
        }
    }
}