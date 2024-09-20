using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamJan.MicroService.PreHospital.Core;
using TriageService.Application.Dtos.TriageConfig.TriageConfig;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface ITriageConfigAppService : IApplicationService
    {
        /// <summary>
        /// 删除指定Redis
        /// </summary>
        /// <param name="reidsKey"></param>
        /// <returns></returns>
        Task<JsonResult> ClearRedisByKeyAsync(string reidsKey);

        /// <summary>
        /// 新增院前分诊设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> SaveTriageConfigAsync(CreateTriageConfigDto dto);

        /// <summary>
        /// 修改院前分诊设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateTriageConfigAsync(TriageConfigDto dto);

        /// <summary>
        /// 删除院前分诊设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteTriageConfigAsync(Guid id);

        ///  <summary>
        ///  获取院前分诊设置集合
        ///  </summary>
        ///  <param name="triageConfigType"></param>
        ///  <param name="appIsUse">app端建档是否需要使用此证件类型，对应扩展字段2</param>
        ///  <param name="isEnable">-1不查询，0：禁用，1：启用</param>
        ///  <returns></returns>
        Task<JsonResult<Dictionary<string, List<TriageConfigDto>>>> GetTriageConfigListAsync(string triageConfigType, int appIsUse = -1,
            int isEnable = 1);


        /// <summary>
        /// 获取特约记账类型(龙岗)
        /// </summary>
        /// <param name="triageConfigType"></param>
        /// <returns></returns>
        Task<JsonResult<Dictionary<string, List<TriageConfigDto>>>> GetTriageConfigSpecialAccountTypeListAsync(string triageConfigType);

        /// <summary>
        /// 获取院前分诊设置详情
        /// </summary>
        /// <param name="type">字典类别代码</param>
        /// <param name="code">字典代码</param>
        /// <returns></returns>
        Task<JsonResult<Dictionary<string, List<TriageConfigDto>>>> GetTriageConfigDetailAsync(string type, string code);

        /// <summary>
        /// 根据分页获取分诊配置列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<JsonResult<PagedResultDto<TriageConfigDto>>> GetTriageConfigPageListAsync(TriageConfigWhereInput input);

        /// <summary>
        /// 从Redis缓存中获取字典
        /// </summary>
        /// <param name="input"></param>
        ///<param name="isEnable">-1不查询，0：禁用，1：启用</param>
        /// <param name="isDeleted">-1：全部  0；未删除  1：已删除</param>
        /// <returns></returns>
        Task<Dictionary<string, List<TriageConfigDto>>> GetTriageConfigByRedisAsync(string input = "", int isEnable = -1, int isDeleted = 0);

        /// <summary>
        /// 获取医院信息
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<HospitalInfoDto>> GetHospitalInfoAsync();


        /// <summary>
        /// 分诊服务初始化种子数据
        /// </summary>
        /// <returns></returns>
        Task<JsonResult<string>> InitDataSeedAsync();

        /// <summary>
        /// 同步费别数据(龙岗)
        /// </summary>
        Task SyncFaberListAsync(FaberSyncHis faberEventData);
    }
}