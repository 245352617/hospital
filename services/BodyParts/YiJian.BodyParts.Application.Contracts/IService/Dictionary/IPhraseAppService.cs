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
    /// 基础数据配置 服务接口
    /// </summary>
    public interface IPhraseAppService : IApplicationService
    {
        /// <summary>
        /// 查询护理记录模板分组
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="staffCode">员工代码</param>
        /// <returns></returns>
        Task<JsonResult<List<SelectPhraseDto>>> SelectIcuPhraseGroup([Required] string deptCode, [Required] string staffCode);

        /// <summary>
        /// 新增或修改护理记录模板
        /// </summary>
        /// <param name="icuPhraseDto"></param>
        /// <returns></returns>
        Task<JsonResult> CreatePhraseGroup(CreateUpdateIcuPhraseDto icuPhraseDto);

        /// <summary>
        /// 删除护理记录模板分组
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<JsonResult<string>> DeletePhraseInfo(Guid guid);

        /// <summary>
        /// 查询护理记录模板
        /// </summary>
        /// <param name="deptCode">科室代码</param>
        /// <param name="staffCode">员工代码</param>
        /// <param name="query">模板名称</param>
        /// <returns></returns>
        Task<JsonResult<List<PhraseGroupDto>>> SelectIcuPhraseList([Required] string deptCode, [Required] string staffCode, string query);

        /// <summary>
        /// 新增或修改护理记录模板
        /// </summary>
        /// <param name="icuPhraseDto"></param>
        /// <returns></returns>
        Task<JsonResult> CreatePhraseInfo(CreateUpdateIcuPhraseDto icuPhraseDto);

        #region 基础配置接口
        /// <summary>
        /// 将一个科室的导管参数导入另一科室
        /// </summary>
        /// <param name="oldDeptCode">源科室</param>
        /// <param name="newDeptCode">目标科室</param>
        /// <returns></returns>
        Task<JsonResult> SaveCanulaItemList([Required] string oldDeptCode, [Required] string newDeptCode);


        /// <summary>
        /// 获取科室班次列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="type">科室班次类型，如观察项：0，出入量：1 血液净化：2，ECMO：3，PICCO：4</param>
        /// <returns></returns>
        Task<JsonResult<List<IcuDeptScheduleDto>>> GetDeptScheduleList(string deptCode, DeptScheduleTypeEnum type);

        /// <summary>
        /// 新增科室班次信息
        /// </summary>
        /// <param name="deptScheduleConfigDto"></param>
        /// <returns></returns>
        Task<JsonResult> AddDeptScheduleInfo(DeptScheduleConfigDto deptScheduleConfigDto);

        /// <summary>
        /// 编辑科室班次信息
        /// </summary>
        /// <param name="deptScheduleConfigDto"></param>
        /// <returns></returns>
        Task<JsonResult> PutDeptScheduleInfo(DeptScheduleConfigDto deptScheduleConfigDto);

        /// <summary>
        /// 删除科室班次信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<JsonResult> DelDeptScheduleInfo(Guid id);

        #endregion
    }
}
