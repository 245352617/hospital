using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using System.Linq;
using System.Linq.Dynamic.Core;
using YiJian.EMR.Libs.Dto;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.ECIS.ShareModel.Enums;
using Volo.Abp.Uow;
using Volo.Abp;
using YiJian.EMR.Libs.Entities;
using YiJian.EMR.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using YiJian.EMR.Permissions;
using Volo.Abp.Data;

namespace YiJian.EMR.Libs
{
    /// <summary>
    /// 病历模板库
    /// </summary>
    [Authorize]
    public class CatalogueAppService : EMRAppService, ICatalogueAppService
    {

        private readonly ICatalogueRepository _catalogueRepository;
        private readonly IXmlTemplateRepository _xmlTemplateRepository;

        private readonly ILogger<CatalogueAppService> _logger;
        private readonly IDataFilter<ISoftDelete> _softDeleteFilter;
      
        /// <summary>
        /// 病历模板库
        /// </summary> 
        public CatalogueAppService(
            ICatalogueRepository catalogueRepository,
            IXmlTemplateRepository xmlTemplateRepository,
            ILogger<CatalogueAppService> logger,
            IDataFilter<ISoftDelete> softDeleteFilter 
        )
        {
            _catalogueRepository = catalogueRepository;
            _xmlTemplateRepository = xmlTemplateRepository;
            _logger = logger;
            _softDeleteFilter = softDeleteFilter; 
        }

        #region 获取目录节点结构树

        /// <summary>
        /// 获取目录分组信息【所属分组】
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="classify">电子文书分类(默认0=电子病历，1=)</param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Catalogues.List)]
        public async Task<ResponseBase<List<CatalogueRootDto>>> GetCatalogueRootOptionsAsync(int lv, EClassify classify = EClassify.EMR)
        { 
            var list = await (await _catalogueRepository.GetQueryableAsync())
                .Where(w => w.IsFile == false && w.Lv <= lv && w.Classify == classify)
                .Select(s => new CatalogueRootDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    ParentId = s.ParentId,
                    Sort = s.Sort
                })
                .ToListAsync();

            var root = list.Where(w => w.ParentId == null).OrderByDescending(o => o.Sort).ToList();
            if (root.Count == 0) return new ResponseBase<List<CatalogueRootDto>>(EStatusCode.CNULL);

            foreach (var item in root)
            {
                RecursiveCatalogueNodes(list, item);
            }

            return new ResponseBase<List<CatalogueRootDto>>(EStatusCode.COK, root);
        }

        /// <summary>
        /// 递归获取目录树结构
        /// </summary>
        /// <param name="list">所有的目录</param>
        /// <param name="item">需要遍历的节点</param>
        private void RecursiveCatalogueNodes(List<CatalogueRootDto> list, CatalogueRootDto item)
        {
            var sub = list.Where(w => w.ParentId == item.Id).OrderByDescending(s => s.Sort).ToList();
            if (sub.Count == 0)
            {
                return;
            }
            else
            {
                item.Catalogues.AddRange(sub);

                foreach (var subItem in item.Catalogues)
                {
                    RecursiveCatalogueNodes(list, subItem);
                }
            }
        }

        #endregion

        #region 获取病历库目录结构树

        /// <summary>
        /// 获取所有的模板的目录
        /// </summary>
        /// <param name="classify">电子文书分类（0=电子病历(默认值),1=文书）</param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Catalogues.List)]  
        public async Task<ResponseBase<List<CatalogueTreeDto>>> GetCataloguesAsync(EClassify classify = EClassify.EMR)
        {
            var list = await (await _catalogueRepository.GetQueryableAsync())
                .Where(w=>w.Classify == classify)
                .Select(s => new CatalogueTreeDto
                {
                    Id = s.Id,
                    IsFile = s.IsFile,
                    ParentId = s.ParentId,
                    Title = s.Title,
                    Sort = s.Sort,
                    Lv = s.Lv,
                    Classify = s.Classify
                })
                .ToListAsync();

            var root = list.Where(w => w.ParentId == null).OrderByDescending(o => o.Sort).ToList();
            if (root.Count == 0) return new ResponseBase<List<CatalogueTreeDto>>(EStatusCode.CNULL);

            foreach (var item in root)
            {
                RecursiveCatalogue(list, item);
            }

            return new ResponseBase<List<CatalogueTreeDto>>(EStatusCode.COK, root);
        }

        /// <summary>
        /// 递归获取目录树结构
        /// </summary>
        /// <param name="list">所有的目录</param>
        /// <param name="item">需要遍历的节点</param> 
        private void RecursiveCatalogue(List<CatalogueTreeDto> list, CatalogueTreeDto item)
        {
            var sub = list.Where(w => w.ParentId == item.Id).OrderByDescending(o => o.Sort).ToList();
            List<CatalogueTreeDto> subData = new();

            if (sub.Count == 0)
            {
                return;
            }
            else
            {
                item.Catalogues.AddRange(sub);

                foreach (var subItem in item.Catalogues)
                {
                    if (subItem.IsFile)
                    {
                        continue;
                    }
                    RecursiveCatalogue(list, subItem);
                }
            }
        }

        #endregion

        /// <summary>
        /// 获取病历库中指定的xml病历文件
        /// </summary>
        /// <param name="catalogueId"></param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Catalogues.Detail)]
        public async Task<ResponseBase<XmlTemplateDto>> GetLibXmlAsync(Guid catalogueId)
        {
            var entity = await (await _xmlTemplateRepository.GetQueryableAsync()).FirstOrDefaultAsync(w=>w.CatalogueId == catalogueId);
            var data = ObjectMapper.Map<XmlTemplate,XmlTemplateDto>(entity);
            return new ResponseBase<XmlTemplateDto>(EStatusCode.COK,data);
        }

        #region 获取模板库病历选项

        /// <summary>
        /// 获取模板库病历选项
        /// </summary> 
        /// <param name="classify">电子文书分类（0=电子病历(默认值),1=文书）</param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Catalogues.List)]  
        public async Task<ResponseBase<List<CatalogueFileTreeDto>>> GetTemplateXmlOptionsAsync(EClassify classify = EClassify.EMR)
        {
            var query = from c in (await _catalogueRepository.GetQueryableAsync()).Where(w => w.Classify == classify)
                        join x in (await _xmlTemplateRepository.GetQueryableAsync()).Select(x => new { x.Id, x.CatalogueId })
                        on c.Id equals x.CatalogueId
                        into temp
                        from tt in temp.DefaultIfEmpty()
                        select new CatalogueFileTreeDto
                        {
                            Id = c.Id,
                            IsFile = c.IsFile,
                            ParentId = c.ParentId,
                            Title = c.Title,
                            Sort = c.Sort,
                            NeedDelete = (!c.IsFile || (c.IsFile && tt.Id != null)) ? false : true, //tt.Id != null 不能删，真的很关键
                            HasXml = tt.Id != null,
                        };

            //await Console.Out.WriteLineAsync( "------------------------------------------");
            //await Console.Out.WriteLineAsync(query.ToQueryString());
            //await Console.Out.WriteLineAsync( "------------------------------------------");

            var list = await query.ToListAsync();

            var root = list.Where(w => !w.ParentId.HasValue).OrderByDescending(o => o.Sort).ToList();
            if (root.Count == 0) return new ResponseBase<List<CatalogueFileTreeDto>>(EStatusCode.CNULL);

            foreach (var item in root)
            {
                RecursiveCatalogueFile(list, item);
            }
            var data = RecursiveTreeFilter(root);
            return new ResponseBase<List<CatalogueFileTreeDto>>(EStatusCode.COK, data);
        } 

        /// <summary>
        /// 获取模板库病历选项
        /// </summary> 
        /// <param name="classify">电子文书分类（0=电子病历(默认值),1=文书）</param>
        /// <param name="getDeleteData">是否获取删除的数据</param>
        /// <returns></returns>
        //[Authorize(EMRPermissions.Catalogues.List)] 
        public async Task<ResponseBase<List<CatalogueFileTreeDto>>> GetAllTemplateXmlOptions1Async(EClassify classify = EClassify.EMR, bool getDeleteData = false)
        {
            if (getDeleteData)
            {
                using (_softDeleteFilter.Disable())
                {
                    return await GetTemplateXmlOptionsAsync(classify);
                }
            }
            return await GetTemplateXmlOptionsAsync(classify);
        }

        /// <summary>
        /// 递归树过滤器
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<CatalogueFileTreeDto> RecursiveTreeFilter(List<CatalogueFileTreeDto> root)
        {
            var dels = new List<CatalogueFileTreeDto>();
            foreach (var item in root)
            {
                if (item.Catalogues.Count == 0)
                {
                    if (!item.IsFile || !item.HasXml)
                    {
                        dels.Add(item);
                    }
                    RecursiveNodeFilter(item);
                }
            }

            //移除第一层没有文件的目录
            foreach (var del in dels)
            {
                root.Remove(del);
            }
            return root;
        }

        private void RecursiveNodeFilter(CatalogueFileTreeDto node)
        {
            var dels = new List<CatalogueFileTreeDto>();
            var childrenNodes = node.Catalogues;
            foreach (var item in childrenNodes)
            {
                if (item.Catalogues.Count == 0)
                {
                    if (!item.IsFile || !item.HasXml)
                    {
                        dels.Add(item);
                    }
                    RecursiveNodeFilter(item);
                }
            }

            foreach (var del in dels)
            {
                childrenNodes.Remove(del);
            }
        }

        /// <summary>
        /// 递归获取目录树结构
        /// </summary>
        /// <param name="list">所有的目录</param>
        /// <param name="item">需要遍历的节点</param>
        private void RecursiveCatalogueFile(List<CatalogueFileTreeDto> list, CatalogueFileTreeDto item)
        {
            var sub = list.Where(w => w.ParentId == item.Id && !w.NeedDelete).ToList();
            if (sub.Count == 0)
            {
                return;
            }
            else
            {
                item.Catalogues.AddRange(sub);
                foreach (var subItem in item.Catalogues)
                {
                    if (subItem.IsFile)
                    {
                        continue;
                    }
                    RecursiveCatalogueFile(list, subItem);
                }
            }
        }

        #endregion

        /// <summary>
        /// 添加/更新目录或分类
        /// </summary>
        /// <param name="model">修改的实体[ [新增不需要传Id] ,[更新必须传Id并且是uuid] ]</param>
        /// <returns></returns> 
        //[Authorize(EMRPermissions.Catalogues.Modify)] 
        public async Task<ResponseBase<Guid>> ModifyCatalogueAsync(CatalogueDto model)
        {
            int level = 0;
            if (model.ParentId.HasValue)
            {
                var parentEntity = await (await _catalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.ParentId); //获取父节点的lv
                level = parentEntity.Lv + 1;
            }

            if (model.Id.HasValue)
            {
                var updateEntity = await (await _catalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w=>w.Id== model.Id); 
                updateEntity.Update(model.Title,model.IsFile,model.Sort, level, model.ParentId.HasValue? model.ParentId:null);
                var retModel = await _catalogueRepository.UpdateAsync(updateEntity);
                return new ResponseBase<Guid>(EStatusCode.COK, retModel.Id);
            }
            else
            {
                var entity = ObjectMapper.Map<CatalogueDto, Catalogue>(model);
                entity.Lv = level;
                entity.ParentId = model.ParentId;

                var retModel = await _catalogueRepository.InsertAsync(entity);
                return new ResponseBase<Guid>(EStatusCode.COK, retModel.Id);
            }
        }

        /// <summary>
        /// 删除指定的目录文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork]
        //[Authorize(EMRPermissions.Catalogues.Delete)]
        public async Task<ResponseBase<Guid>> RemoveCatalogueAsync(Guid id)
        {
            var entity = await (await _catalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null)
            {
                return new ResponseBase<Guid>(EStatusCode.CNULL, Guid.Empty);
            }

            var count = (await _catalogueRepository.GetQueryableAsync()).Where(w => w.ParentId == entity.Id).Count();
            if (count > 0)
            {
                return new ResponseBase<Guid>(EStatusCode.C10000, Guid.Empty);
            }
            await _catalogueRepository.DeleteAsync(id);
            await _xmlTemplateRepository.DeleteAsync(w => w.CatalogueId == id);

            return new ResponseBase<Guid>(EStatusCode.COK, id);
        }

        /// <summary>
        /// 单个导入xml文件操作
        /// </summary>
        /// <param name="catalogueId"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        [RemoteService(false)]
        public async Task<ResponseBase<XmlTemplateDto>> ImportXmlTemplateAsync(Guid catalogueId, string xml)
        {
            var ret = new ResponseBase<XmlTemplateDto>(EStatusCode.COK);

            var entity = await (await _xmlTemplateRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.CatalogueId == catalogueId);
            if (entity != null)
            {
                entity.UpdateTemplateXml(xml);
                var retModel = await _xmlTemplateRepository.UpdateAsync(entity);
                ret.Data = ObjectMapper.Map<XmlTemplate, XmlTemplateDto>(retModel);
            }
            else
            {
                var catalogue =(await _catalogueRepository.GetQueryableAsync()).FirstOrDefault(w => w.Id == catalogueId);
                if (catalogue == null)
                {
                    return new ResponseBase<XmlTemplateDto>(EStatusCode.CFail, message: $"电子病历catalogueId={catalogueId}的目录不存在");
                }
                var xmlTemplate = new XmlTemplate(GuidGenerator.Create(), xml, catalogueId);
                var retModel = await _xmlTemplateRepository.InsertAsync(xmlTemplate);
                ret.Data = ObjectMapper.Map<XmlTemplate, XmlTemplateDto>(retModel);
            }
            return ret;
        }

    }
}
