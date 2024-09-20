using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.Regions;
using YiJian.MasterData.Separations.Contracts;

namespace YiJian.MasterData.MasterData.Regions;

/// <summary>
/// 地区
/// </summary>
[Authorize]
public class RegionAppService : MasterDataAppService, IRegionAppService
{
    private readonly IRegionRepository _regionRepository;
    private readonly IAreaRepository _areaRepository;

    public RegionAppService(IRegionRepository regionRepository, IAreaRepository areaRepository)
    {
        _regionRepository = regionRepository;
        _areaRepository = areaRepository;
    }

    /// <summary>
    /// 获取区域
    /// </summary>
    /// <param name="code">code</param>
    /// <param name="regionType">0：省，1：市，2：区/县</param>
    /// <returns></returns>
    public async Task<List<RegionDto>> GetAsync(string code = "", ERegionType regionType = ERegionType.Province)
    {
        var region = await (await _regionRepository.GetQueryableAsync())
            .WhereIf(!code.IsNullOrEmpty(), x => x.RegionType == regionType && x.ParentCode == code)
            .WhereIf(code.IsNullOrEmpty(), x => x.RegionType == ERegionType.Province).ToListAsync();
        var regionList = ObjectMapper.Map<List<Region>, List<RegionDto>>(region);
        return regionList;
    }

    /// <summary>
    /// 模糊查询获取区域
    /// </summary>
    /// <param name="region"></param>
    /// <returns></returns>
    public async Task<List<RegionDto>> GetRegionsAsync(string region)
    {
        if (string.IsNullOrEmpty(region) || region.Length < 2)
        {
            throw new Exception("请输入模糊查询地址");
        }

        var regions = await (await _areaRepository.GetQueryableAsync()).Where(x => x.AreaCode.Contains(region) || x.AreaFullName.Contains(region)).Take(100).ToListAsync();
        List<RegionDto> regionDtos = new List<RegionDto>();
        foreach (var item in regions)
        {
            RegionDto regionDto = new RegionDto();
            regionDto.RegionCode = item.AreaCode;
            regionDto.RegionName = item.AreaFullName;
            regionDtos.Add(regionDto);
        }
        return regionDtos;
    }
}