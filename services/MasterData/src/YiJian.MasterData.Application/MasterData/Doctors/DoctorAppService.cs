using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;
using YiJian.MasterData.MasterData.Doctors;

namespace YiJian.MasterData;

/// <summary>
/// 医生信息
/// </summary>
[Authorize]
public class DoctorAppService : MasterDataAppService, IDoctorAppService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorAppService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    /// <summary>
    /// 获取医生分页信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<DoctorDto>> GetPageListAsync(GetDoctorPagedInput input)
    {
        var doctorList = await (await _doctorRepository.GetQueryableAsync())
            .Where(x=>x.IsActive)
            .WhereIf(!string.IsNullOrEmpty(input.DoctorCode) , x => x.DoctorCode == input.DoctorCode)
            .WhereIf(!string.IsNullOrEmpty(input.DoctorName), x => x.DoctorName == input.DoctorName)
            .WhereIf(input.DoctorType != -1, x => x.DoctorType == input.DoctorType)
            .ToListAsync();
        var pageList = ObjectMapper.Map<List<Doctor>, List<DoctorDto>>(doctorList
            .Skip(input.Size * (input.Index - 1))
            .Take(input.Size).ToList()).AsReadOnly();
        var pageResult = new PagedResultDto<DoctorDto>(doctorList.Count, pageList);
        return pageResult;
    }
}