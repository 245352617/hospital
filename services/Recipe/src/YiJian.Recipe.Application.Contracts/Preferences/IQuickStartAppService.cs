using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.Preferences.Dto;

namespace YiJian.Preferences
{
    /// <summary>
    /// 快速开嘱内容配置（个人偏好设置）
    /// </summary>
    public interface IQuickStartAppService : IApplicationService
    {
        /// <summary>
        /// 获取快速开嘱列表信息
        /// </summary> 
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<QuickStartDto>> GetAsync(InitCatalogueDto model);

        /// <summary>
        /// 获取所有后台数据[整合的接口]
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<List<AllMedicineDto>> GetAdminMedicinesAsync(InitCatalogueDto model);

        /// <summary>
        /// 获取当前医生的类型目录数据
        /// </summary>
        /// <returns></returns>
        public Task<List<CataloguesDto>> GetCataloguesAsync(InitCatalogueDto model);

        /// <summary>
        /// 获取指定目录下的药品信息集合
        /// </summary>
        /// <param name="catalogueId">目录id</param>
        /// <returns></returns> 
        public Task<List<QuickStartQueryDto>> GetQueryAsync(Guid catalogueId);

        /// <summary>
        /// 初始化目录(医生第一次进来的时候需要初始化一个目录)
        /// </summary>
        /// <param name="model">快速开嘱目录信息</param> 
        /// <returns></returns>
        public Task<List<CataloguesDto>> InitCatalogueAsync(InitCatalogueDto model);

        /// <summary>
        /// 更新快速开始的类型目录
        /// </summary>
        /// <param name="model">快速开嘱对象</param>
        /// <returns></returns> ;
        public Task<Guid> PutCatalogueAsync(PutQuickStartCatalogueDto model);

        /// <summary>
        /// 添加快速医嘱信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> ;
        public Task<bool> AddAdviceAsync(AddQuickStartAdviceDto model);

        /// <summary>
        /// 删除快速开嘱信息(支持多条)
        /// </summary>
        /// <returns></returns> ;
        public Task<bool> DeleteAdviceAsync(List<Guid> ids);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>;
        public Task<bool> SortAsync(List<SortDto> models);

    }
}
