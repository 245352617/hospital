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
    /// 字典API
    /// </summary>
    [Authorize]
    public class DictionariesAppService : EcisPatientAppService, IDictionariesAppService
    {
        private readonly IFreeSql _freeSql;

        public DictionariesAppService(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<IEnumerable<DictionariesDto>>> GetDictionariesListAsync(DictionariesWhereInput input, CancellationToken cancellationToken)
        {
            List<DictionariesDto> list = await _freeSql.Select<Dictionaries>().WhereIf(
                    !string.IsNullOrEmpty(input.DictionariesTypeCode),
                    w => input.DictionariesTypeCode == (w.DictionariesTypeCode.ToString()))
                .Where(w => w.Status)
                .ToListAsync<DictionariesDto>(cancellationToken: cancellationToken);
            return RespUtil.Ok<IEnumerable<DictionariesDto>>(data: list);
        }

        /// <summary>
        /// 新增或者修改实体
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("/api/patientService/dictionaries/CreateOrUpdateDictionaries")]
        public async Task<ResponseResult<string>> CreateOrUpdateDictionariesAsync(CreateOrUpdateDictionariesDto dto, CancellationToken cancellationToken = default)
        {
            if (dto.Id == -1)
            {
                Dictionaries dictionaries = dto.To<Dictionaries>();
                await _freeSql.Insert(dictionaries).ExecuteAffrowsAsync(cancellationToken);
            }
            else
            {
                DictionariesDto dictionariesDto = await _freeSql.Select<Dictionaries>().Where(w => w.Id == dto.Id).FirstAsync<DictionariesDto>(cancellationToken);
                if (dictionariesDto == null)
                {
                    return RespUtil.Error<string>(msg: "数据不存在");
                }

                if (dto.DictionariesTypeCode == DictionariesType.BedSettings && await _freeSql.Select<Bed>().AnyAsync(a => a.BedAreaCode == dto.DictionariesCode, cancellationToken))
                {
                    if (dto.Status != dictionariesDto.Status || dto.DictionariesCode != dictionariesDto.DictionariesCode)
                    {
                        return RespUtil.Error<string>(msg: "该区域下已有床位，无法修改");
                    }
                }
                await _freeSql.Update<Dictionaries>().IgnoreColumns(i => i.Id).SetDto(dto).Where(w => w.Id == dto.Id)
                    .ExecuteAffrowsAsync(cancellationToken);
            }

            return RespUtil.Ok<string>();
        }

        /// <summary>
        /// 根据id删除字典
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> DeleteDictionariesAsync(DictionariesWhereInput input, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _freeSql.Select<Dictionaries>().AnyAsync(w => w.Id == input.Id, cancellationToken))
                {
                    return RespUtil.Error<string>(msg: "数据不存在");
                }

                await _freeSql.Delete<Dictionaries>().Where(w => w.Id == input.Id).ExecuteAffrowsAsync(cancellationToken);
                return RespUtil.Ok<string>();
            }
            catch (Exception e)
            {
                return RespUtil.Error<string>(msg: e.Message);
            }
        }

        /// <summary>
        /// 根据id获取字典信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseResult<DictionariesDto>> GetDictionariesAsync(DictionariesWhereInput input, CancellationToken cancellationToken)
        {
            try
            {
                DictionariesDto dictionariesDto = await _freeSql.Select<Dictionaries>().Where(w => w.Id == input.Id)
                     .FirstAsync<DictionariesDto>(cancellationToken);
                if (dictionariesDto == null)
                {
                    return RespUtil.Error<DictionariesDto>(msg: "数据不存在");
                }

                return RespUtil.Ok(data: dictionariesDto);
            }
            catch (Exception e)
            {
                return RespUtil.Error<DictionariesDto>(msg: e.Message);
            }
        }
    }
}