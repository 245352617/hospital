using YiJian.BodyParts.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.BodyParts.IService
{
    /// <summary>
    /// 基础字典服务接口
    /// </summary>
    public interface IDictAppService : IApplicationService
    {
        #region 模块参数
        /// <summary>
        /// 根据条件查询模块参数
        /// </summary>
        /// <param name="deptCode">科室代码(系统项目:system)</param>
        /// <param name="moduleType">模块类型:(CANULA：导管，VS：观察项目，IO：出入量)</param>
        /// <param name="query">名称</param>
        /// <returns></returns>
        Task<JsonResult<List<IcuParaModuleDto>>> GetParaModuleList(string deptCode, string moduleType, string query);

        /// <summary>
        /// 新增或修改模块参数
        /// </summary>
        /// <param name="moduleDto"></param>
        /// <returns></returns>
        Task<JsonResult> SaveParaModuleInfo(CreateUpdateIcuParaModuleDto moduleDto);

        /// <summary>
        /// 修改模块排序
        /// </summary>
        /// <param name="icuParaModuleDtos"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateParaModuleSort(List<CreateUpdateIcuParaModuleDto> icuParaModuleDtos);

        /// <summary>
        /// 删除模块参数
        /// </summary>
        /// <param name="moduleCode">模块代码</param>
        /// <returns></returns>
        Task<JsonResult> DeleteParaModuleInfo(string moduleCode);
        #endregion

        #region 科室参数
        /// <summary>
        /// 根据条件获取参数项目列表
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="moduleType">模块类型(VS-观察项目，IO-出入量，CANULA-管道)</param>
        /// <param name="query">项目名称</param>
        /// <returns></returns>
        Task<JsonResult<List<IcuParaItemDto>>> GetIcuParaItemList([Required] string deptCode, string moduleCode, string moduleType, string query);

        /// <summary>
        /// 修改一条系统项目或科室项目
        /// </summary>
        /// <param name="itemDto"></param>
        /// <returns></returns>
        Task<JsonResult> SaveParaItemInfo(CreateUpdateIcuParaItemDto itemDto);

        /// <summary>
        /// 查询批量导入参数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="moduleType">模块类型(VS-观察项目，IO-出入量，CANULA-管道)</param>
        /// <param name="moduleCode"></param>
        /// <param name="groupName">导管(管道属性、管道观察),皮肤(皮肤属性、皮肤观察)观察项和出入量传null</param>
        /// <returns></returns>
        Task<JsonResult<List<ModuleListDto>>> SelectModuleList([Required] string deptCode, [Required] string moduleType, string moduleCode, string groupName);

        /// <summary>
        /// 批量导入参数
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="groupName">导管类型(管道属性、管道观察),观察项和出入量传null</param>
        /// <param name="paraCodes">选择的参数列表</param>
        /// <returns></returns>
        Task<JsonResult> SaveParaItemList([Required] string deptCode, [Required] string moduleCode, string groupName, List<string> paraCodes);

        /// <summary>
        /// 修改排序
        /// </summary>
        /// <param name="icuParaItemDtos"></param>
        /// <returns></returns>
        Task<JsonResult> UpdateParaItemSort(List<CreateUpdateIcuParaItemDto> icuParaItemDtos);

        /// <summary>
        /// 删除一条参数
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        //Task<JsonResult> DeleteParaItemInfo(Guid guid);

        /// <summary>
        /// 批量删除(仅观察项和出入量可以批量删除)
        /// </summary>
        /// <param name="guids"></param>
        /// <returns></returns>
        Task<JsonResult> DeleteParaItemList(List<Guid> guids);

        /// <summary>
        /// 更新同步在线病人
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="moduleType">模块类型</param>
        /// <returns></returns>
        //WTask<JsonResult> UpdateParaToPatient([Required] string deptCode, [Required] string moduleType);
        #endregion

        #region 参数字典项目
        /// <summary>
        /// 根据模块代码、参数代码查询参数字典列表
        /// </summary>
        /// <param name="ModuleCode">模块代码</param>
        /// <param name="ParaCode">参数代码</param>
        /// <returns></returns>
        Task<JsonResult<List<DictDto>>> SelectDictList(string ModuleCode, string ParaCode);
        #endregion

    }
}
