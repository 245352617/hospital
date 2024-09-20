using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.NursingSettings.Dto;

namespace YiJian.Health.Report.NursingSettings
{
    /// <summary>
    /// 护理单配置
    /// </summary>
    public interface INursingSettingAppService : IApplicationService
    {
        /// <summary>
        /// 新增或修改主题分类
        /// </summary>
        /// <see cref="ModifySubjectDto"/>
        /// <returns></returns> 
        public Task<ResponseBase<Guid>> ModifySubjectAsync(ModifySubjectDto model);

        /// <summary>
        /// 删除主题分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public Task<ResponseBase<Guid>> RemoveSubjectAsync(Guid id);

        /// <summary>
        /// 删除未发生业务的表头数据集
        /// </summary>
        /// <param name="ids">还未发生业务的表头Id</param>
        /// <returns></returns>  
        public Task<ResponseBase<bool>> RemoveHeadersAsync(List<Guid> ids);

        /// <summary>
        /// 删除未发生业务的表单域数据集
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>   
        public Task<ResponseBase<bool>> RemoveItemsAsync(RemoveItemsDto model);

        /// <summary>
        /// 获取下一层的所有表单域内容
        /// </summary>
        /// <see cref="SearchNursingSettingItemsDto"/>
        /// <returns></returns>
        public Task<ResponseBase<NursingSettingHeaderItemDto>> ExpandItemListAsync(SearchNursingSettingItemsDto model);

        /// <summary>
        /// 新增或更新选项内容（单个）
        /// </summary>
        /// <see cref="ModifyNursingItemsDto"/>
        /// <returns></returns>  
        public Task<ResponseBase<ModifyHeaderDto>> ModifyItemAsync(ModifyNursingItemsDto model);

        /// <summary>
        /// 获取所有的护理单主题集合
        /// </summary>
        /// <returns></returns> 
        public Task<ResponseBase<List<NursingSettingDto>>> GetNursingSheetListAsync(string groupId);

        /// <summary>
        /// 点击动态六项返回的配置内容
        /// </summary>
        /// <param name="headerId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">动态六项对应的表头UUID，如：腹部，出血，循环...</exception>
        public Task<ResponseBase<NursingInputOptionsDto>> GetInputOptionsAsync(Guid headerId);

    }
}
