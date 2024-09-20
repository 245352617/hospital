using YiJian.BodyParts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace YiJian.BodyParts.IService
{
    /// <summary>
    /// 基础字典
    /// </summary>
    public interface IDictSourceAppService : IApplicationService, ITransientDependency
    {
        Task<JsonResult<List<DictSourceDto>>> GetDictSourceAsync(string name);

        Task<JsonResult> CreatetDictSource(string name, List<DictSourceDto> sourceDtos);

        Task<JsonResult<List<DictSourceGroup>>> GetBasicFile(string query);

        Task<JsonResult> CreateBasicFile(DictSourceGroup sourceGroup);

        Task<JsonResult<List<IcuSysParaDto>>> GetSysParaAsync(SysTypeEnum paraEnum, string deptCode);

        Task<JsonResult> CreateSysPara(IcuSysParaDto sysParaDto);

        Task<JsonResult<List<DictScoreGroup>>> GetApchellConfig(string deptCode, string moduleCode);

        Task<JsonResult> CreateApchellConfig(DictScoreGroup scoreGroup);

        Task<JsonResult> CreateNursingWorkStaticsConfig(NursingWorkStaticsDto nursingWorkStaticsDto);

        Task<JsonResult> DeleteNursingWorkStaticsConfig(Guid id);

        Task<JsonResult<List<NursingWorkStaticsDto>>> SelectNursingWorkStaticsConfig(string moduleType, string deptCode);

        Task<JsonPageResult<List<DictConsumableDto>>> SelectDictConsumList(int? PageIndex, int? PageSize, string ConsumType, string query);

        Task<JsonResult<List<DictConsumableDto>>> SelectAllDictConsum();

        Task<JsonResult> CreateDictConsumInfo(DictConsumableDto consumDto);

        Task<JsonResult> DeleteDictConsumInfo(Guid guid);

        Task<JsonPageResult<List<DictNutritionDto>>> SelectDictNutritionList(int? PageIndex, int? PageSize, string query);

        Task<JsonResult> CreateDictNutrition(DictNutritionDto nutritionDto);

        Task<JsonResult> DeleteDictNutrition(Guid guid);

        Task<JsonResult<List<IcuTableConfigDto>>> SelectTableConfigList(ParaTypeEnum typeEnum);

        Task<JsonResult> UpdateTableConfig(IcuTableConfigDto tableConfigDto);
    }
}
