using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.DailyExpressions.Dto;
using YiJian.EMR.Enums;

namespace YiJian.EMR.DailyExpressions
{
    /// <summary>
    /// 常用词
    /// </summary>
    public interface IPhraseAppService : IApplicationService
    {

        /// <summary>
        /// 模板
        /// </summary>
        /// <param name="templateType">模板类型，0=通用，1=科室，2=个人</param>
        /// <param name="belonger">归属人 如果 TemplateType=2 归属者为医生Id doctorId, 如果 TemplateType=1 归属者为科室id deptid , 如果 TemplateType=0 归属者为"hospital"</param>
        /// <param name="searchText">搜索的文本，只针对短语搜索</param>
        /// <returns></returns>
        public Task<ResponseBase<List<PhraseCatalogueSampleDto>>> GetAllAsync(ETemplateType templateType, string belonger, string searchText);

        /// <summary>
        /// 获取常用语目录
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="doctorId"></param> 
        /// <returns></returns>
        public Task<ResponseBase<List<PhraseCatalogueDto>>> GetCatalogueMapAsync(string deptId, string doctorId);

        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> CreateCatalogueAsync(PhraseCatalogueInfoDto entity);

        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> PutCatalogueAsync(PhraseCatalogueInfoDto entity);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="ids">目录的Id集合</param>
        /// <param name="role">期望展示的权限 -1=拒绝，没有任何权限  , 0=管理员 , 1=医生 , 2=护士</param>
        /// <returns></returns>
        public Task<ResponseBase<bool>> DeleteCatalogueAsync(int[] ids, EExpectedRole role);

        /// <summary>
        /// 获取制定目录下的所有常用词记录
        /// </summary>
        /// <param name="id">目录的Id</param>
        /// <returns></returns>
        public Task<ResponseBase<PhraseCatalogueSampleDto>> GetPhraseListByCatalogueAsync(int id);

        /// <summary>
        /// 添加常用词
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> CreatePhraseAsync(PhraseDto entity);

        /// <summary>
        /// 更新常用词
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> PutPhraseAsync(PhraseDto entity);

        /// <summary>
        /// 删除常用词
        /// </summary>
        /// <param name="ids">指定的常用词Id集合</param>
        /// <param name="role">期望展示的权限 -1=拒绝，没有任何权限  , 0=管理员 , 1=医生 , 2=护士</param>
        /// <returns></returns> 
        public Task<ResponseBase<bool>> DeletePhraseAsync(int[] ids, EExpectedRole role);

    }
}
