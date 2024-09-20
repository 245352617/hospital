using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.EMR.Libs.Dto;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Enums;
using Volo.Abp.Domain.Entities;

namespace YiJian.EMR.Templates
{
    /// <summary>
    /// 病历模板
    /// </summary>
    public interface ITemplateCatalogueAppService : IApplicationService
    {
        /// <summary>
        /// 获取目录分组信息【所属分组】
        /// </summary>
        /// <see cref="CatalogueRootRequestDto"/> 
        /// <returns></returns>
        public Task<ResponseBase<List<CatalogueRootDto>>> CatalogueRootOptionsAsync(CatalogueRootRequestDto model);

        /// <summary>
        /// 获取所有的通用模板目录
        /// </summary>
        /// <param name="classify"></param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<GeneralCatalogueTreeDto>>> GetGeneralCataloguesAsync(EClassify classify,bool isBackground = true);

        /// <summary>
        /// 获取当前用户的科室模板目录 
        /// </summary> 
        /// <param name="role"></param>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<DepartmentCatalogueGroupDto>>> GetDepartmentCataloguesAsync(EExpectedRole role,string deptCode, string deptName, bool isBackground = true);

        /// <summary>
        /// 获取当前用户的个人模板目录
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="doctorCode">医生编码</param>
        /// <param name="doctorName"></param>
        /// <param name="isBackground"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<PersonalCatalogueGroupDto>>> GetPersonalCataloguesAsync(EExpectedRole role, string doctorCode,string doctorName, bool isBackground = true);

        /// <summary>
        /// 新增/编辑病历分组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> ModifyCatalogueItemAsync(ModifyCatalogueItemDto model);

        /// <summary>
        /// 删除病历模板或分组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorCode"></param>
        /// <param name="classify"></param>
        /// <param name="templateType"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> RemoveCatalogueItemAsync(Guid id, string doctorCode, EClassify classify, ETemplateType templateType, string deptCode);

        /// <summary>
        /// 新增/编辑病历
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> ModifyEmrItemAsync(ModifyEmrItemDto model);

        /// <summary>
        /// 获取电子病历模板信息
        /// </summary>
        /// <param name="templateCatalogueId"></param>
        /// <returns></returns>
        public Task<ResponseBase<MyXmlTemplateDto>> GetMyXmlTemplateAsync(Guid templateCatalogueId);

        /// <summary>
        /// 更新当前电子病历模板
        /// </summary>
        /// <see cref="MyXmlTemplateDto"/>
        /// <returns></returns> 
        public Task<ResponseBase<Guid>> UpdateTemplateXmlAsync(UpdateMyXmlTemplateDto model);

        /// <summary>
        /// 获取另存为新的电子病例模板的目录结构树
        /// </summary> 
        /// <param name="doctorCode">医生编码 [获取个人模板的时候需要]</param>
        /// <param name="deptCode">科室编码 [获取科室模板的时候需要]</param> 
        /// <param name="doctorName">医生名称 [获取个人模板的时候需要]</param>
        /// <returns></returns>
        public Task<ResponseBase<SaveAsTemplateTreeDto>> GetSaveAsTemplateAsync(string doctorCode, string deptCode,string doctorName);

        /// <summary>
        /// 另存为模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<ResponseBase<Guid>> SaveAsTemplateAsync(SaveAsTemplateDto model);

        /// <summary>
        /// 权限控制(true=拒绝，false=允许)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<RefusedResponseDto>>> RefusedAsync(RefusedRequestDto model);
    }
}
