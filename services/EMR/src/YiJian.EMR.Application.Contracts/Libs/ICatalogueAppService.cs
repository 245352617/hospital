using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.EMR.Libs.Dto;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Libs
{
    /// <summary>
    /// 目录树结构
    /// </summary>
    public interface ICatalogueAppService : IApplicationService
    {
        /// <summary>
        /// 获取目录分组信息【所属分组】
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="classify">电子文书分类(默认0=电子病历，1=)</param>
        /// <returns></returns>
        public Task<ResponseBase<List<CatalogueRootDto>>> GetCatalogueRootOptionsAsync(int lv, EClassify classify);

        /// <summary>
        /// 获取所有的模板的目录
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<CatalogueTreeDto>>> GetCataloguesAsync(EClassify classify);


        /// <summary>
        /// 获取病历库中指定的xml病历文件
        /// </summary>
        /// <param name="catalogueId"></param>
        /// <returns></returns>
        public Task<ResponseBase<XmlTemplateDto>> GetLibXmlAsync(Guid catalogueId);

        /// <summary>
        /// 获取模板库病历选项
        /// </summary> 
        /// <param name="classify">电子文书分类（0=电子病历(默认值),1=文书）</param>
        /// <returns></returns>
        public Task<ResponseBase<List<CatalogueFileTreeDto>>> GetTemplateXmlOptionsAsync(EClassify classify = EClassify.EMR);

        /// <summary>
        /// 添加/更新目录或分类
        /// </summary>
        /// <param name="model">修改的实体[[新增不需要传Id] ,[更新必须传Id并且是uuid] ]</param>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> ModifyCatalogueAsync(CatalogueDto model);

        /// <summary>
        /// 删除指定的目录文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> RemoveCatalogueAsync(Guid id);

        /// <summary>
        /// 单个导入xml文件操作
        /// </summary>
        /// <param name="catalogueId"></param>
        /// <param name="xml"></param>
        /// <returns></returns> 
        public Task<ResponseBase<XmlTemplateDto>> ImportXmlTemplateAsync(Guid catalogueId, string xml);

    }
}
