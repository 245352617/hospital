using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.DailyExpressions.Contracts;
using YiJian.EMR.DailyExpressions.Dto;
using YiJian.EMR.DailyExpressions.Entities;
using YiJian.EMR.Enums;

namespace YiJian.EMR.DailyExpressions
{
    /// <summary>
    /// 常用词服务
    /// </summary>
    [Authorize]
    public class PhraseAppService : EMRAppService, IPhraseAppService
    {
        private readonly IPhraseCatalogueRepository _phraseCatalogueRepository;
        private readonly IPhraseRepository _phraseRepository;

        public PhraseAppService(IPhraseCatalogueRepository phraseCatalogueRepository,
            IPhraseRepository phraseRepository)
        {
            _phraseCatalogueRepository = phraseCatalogueRepository;
            _phraseRepository = phraseRepository;
        }

        /// <summary>
        /// 获取所有的
        /// </summary>
        /// <param name="templateType">模板类型，0=通用，1=科室，2=个人</param>
        /// <param name="belonger">归属人 如果 TemplateType=2 归属者为医生Id doctorId, 如果 TemplateType=1 归属者为科室id deptid , 如果 TemplateType=0 归属者为"hospital"</param>
        /// <param name="searchText">搜索的文本，只针对短语搜索</param>
        /// <returns></returns> 
        public async Task<ResponseBase<List<PhraseCatalogueSampleDto>>> GetAllAsync(ETemplateType templateType, string belonger, string searchText)
        {
            await InitAsync(templateType,belonger); //初始化目录
            var res = await _phraseCatalogueRepository.GetAllAsync(templateType, belonger, searchText);
            if (!res.Any()) return new ResponseBase<List<PhraseCatalogueSampleDto>>(EStatusCode.CNULL); 
            var data = ObjectMapper.Map<List<PhraseCatalogue>, List<PhraseCatalogueSampleDto>>(res);
            return new ResponseBase<List<PhraseCatalogueSampleDto>>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 初始化目录
        /// </summary>
        /// <param name="templateType"></param>
        /// <param name="belonger"></param>
        /// <returns></returns>
        private async Task InitAsync(ETemplateType templateType,string belonger)
        {
            var list = await (await _phraseCatalogueRepository.GetQueryableAsync()).Where(w => w.Title == "默认").ToListAsync();

            List<PhraseCatalogue> entities = new();

            switch (templateType)
            {
                case ETemplateType.General:
                    {
                        //通用
                        var general = list.FirstOrDefault(w => w.TemplateType == ETemplateType.General && w.Belonger == "hospital");
                        if (general == null) entities.Add(new PhraseCatalogue("默认", ETemplateType.General, "hospital", 0));
                    }
                    break;
                case ETemplateType.Department:
                    {
                        //科室
                        var department = list.FirstOrDefault(w => w.TemplateType == ETemplateType.Department && w.Belonger == belonger.Trim());
                        if (department == null) entities.Add(new PhraseCatalogue("默认", ETemplateType.Department, belonger.Trim(), 0));
                    }
                    break;
                case ETemplateType.Personal:
                    {
                        //个人
                        var person = list.FirstOrDefault(w => w.TemplateType == ETemplateType.Personal && w.Belonger == belonger.Trim());
                        if (person == null) entities.Add(new PhraseCatalogue("默认", ETemplateType.Personal, belonger.Trim(), 0));
                    }
                    break;
                default:
                    break;
            }
             
            if (entities.Any()) await _phraseCatalogueRepository.InsertManyAsync(entities,autoSave:true); 
        }


        /// <summary>
        /// 获取常用语目录
        /// </summary>
        /// <param name="deptId">可是id</param>
        /// <param name="doctorId">医生id</param> 
        /// <returns></returns>
        public async Task<ResponseBase<List<PhraseCatalogueDto>>> GetCatalogueMapAsync(string deptId, string doctorId)
        {
            List<PhraseCatalogueDto> data = new();
            var res = await _phraseCatalogueRepository.CatalogueMapAsync(deptId, doctorId); 
            if (!res.Any()) return new ResponseBase<List<PhraseCatalogueDto>>(EStatusCode.CNULL);
            
            foreach (var item in res)
            {
                var catalogues = ObjectMapper.Map<List<PhraseCatalogue>, List<PhraseCatalogueInfoDto>>(item.Value);
                data.Add(new PhraseCatalogueDto { TemplateType = item.Key, Catalogues = catalogues });
            }

            return new ResponseBase<List<PhraseCatalogueDto>>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> CreateCatalogueAsync(PhraseCatalogueInfoDto model)
        {
            //if (!model.hasPermission()) return new ResponseBase<int>(EStatusCode.C401,0,"没有操作常用词的操作权限");  
            var entity = ObjectMapper.Map<PhraseCatalogueInfoDto, PhraseCatalogue>(model);
            var exists = await _phraseCatalogueRepository.CheckTitleAsync(model.Title, model.TemplateType, CurrentUser.Id.Value);
            if (exists) return new ResponseBase<int>(EStatusCode.CExist, 0, "同样的分类同样的操作用户不能新建同样的目录名称");
            var res = await _phraseCatalogueRepository.CreateAsync(entity);
            return new ResponseBase<int>(EStatusCode.COK, res.Id);
        }

        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> PutCatalogueAsync(PhraseCatalogueInfoDto model)
        {
            //if (!model.hasPermission()) return new ResponseBase<int>(EStatusCode.C401, 0, "没有操作常用词的操作权限");
            var entity = await (await _phraseCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id);
            if (entity == null) return new ResponseBase<int>(EStatusCode.CNULL, model.Id ?? 0, "找不到更新的目录");
            if (model.Title.Trim() != entity.Title.Trim())
            {
                var exists = await _phraseCatalogueRepository.CheckTitleAsync(model.Title, model.TemplateType, CurrentUser.Id.Value);
                if (exists) return new ResponseBase<int>(EStatusCode.CExist, 0, "同样的分类同样的操作用户不能新建同样的目录名称");
            }
            entity.Update(model.Title);
            var res = await _phraseCatalogueRepository.ModifyAsync(entity);
            return new ResponseBase<int>(EStatusCode.COK, res.Id);
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="ids">目录的Id集合</param>
        /// <param name="role">期望展示的权限 -1=拒绝，没有任何权限  , 0=管理员 , 1=医生 , 2=护士</param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<ResponseBase<bool>> DeleteCatalogueAsync(int[] ids, EExpectedRole role)
        {
            /* 删除所有权限相关的代码检查
            var data = await _phraseCatalogueRepository.GetListAsync(ids); 
            var templateType = data.GroupBy(g => g.TemplateType);
            var keys = templateType.Select(s => s.Key).ToList(); 

           //如果是管理员，那不能操作个人的数据
            if (role == EExpectedRole.Admin)
            {
               var exists = keys.Any(w => w == ETemplateType.Personal);
               if (exists) return new ResponseBase<bool>(EStatusCode.C401, false, "管理员不能删除个人目录");
            }

            //如果是医生，护士等非管理员权限的，只能操作个人的
            if ((int)role > 0)
            {
               var exists = keys.Any(w => (w == ETemplateType.Department) || w == ETemplateType.General);
               if (exists) return new ResponseBase<bool>(EStatusCode.C401, false, "个人不能删除科室或者全院目录");
            }
           */

            await _phraseCatalogueRepository.DeleteAsync(ids);
            return new ResponseBase<bool>(EStatusCode.COK, true);
        }


        /// <summary>
        /// 获取制定目录下的所有常用词记录
        /// </summary>
        /// <param name="id">目录的Id</param>
        /// <returns></returns>
        public async Task<ResponseBase<PhraseCatalogueSampleDto>> GetPhraseListByCatalogueAsync(int id)
        {
            var res = await _phraseCatalogueRepository.GetPhraseListByCatalogueAsync(id);
            if (res == null) return new ResponseBase<PhraseCatalogueSampleDto>(EStatusCode.CNULL);

            var data = ObjectMapper.Map<PhraseCatalogue, PhraseCatalogueSampleDto>(res);
            return new ResponseBase<PhraseCatalogueSampleDto>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 添加常用词
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> CreatePhraseAsync(PhraseDto model)
        {  
            var catalogue = await (await _phraseCatalogueRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.CatalogueId); 
            if (catalogue == null) return new ResponseBase<int>(EStatusCode.CNULL, 0, "你选择的目录有问题");
            //if (!HasPermission(model.Role, catalogue.TemplateType)) return new ResponseBase<int>(EStatusCode.C401, 0, "没有操作常用词的操作权限");

            var exists = await _phraseRepository.CheckTitleAsync(model.Title, model.CatalogueId);
            if (exists) return new ResponseBase<int>(EStatusCode.CExist, 0, "同一个目录不能出现同样的常用词标题");
            var entity = ObjectMapper.Map<PhraseDto, Phrase>(model);
            var res = await _phraseRepository.CreateAsync(entity);
            return new ResponseBase<int>(EStatusCode.COK, res.Id);
        }

        /// <summary>
        /// 更新常用词
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> PutPhraseAsync(PhraseDto model)
        {
            var catalogue = await _phraseCatalogueRepository.GetAsync(model.CatalogueId,false);
            //if(!HasPermission(model.Role, catalogue.TemplateType)) return new ResponseBase<int>(EStatusCode.C401, 0, "没有操作常用词的操作权限");

            var entity = await (await _phraseRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id);
            if (entity == null) return new ResponseBase<int>(EStatusCode.CNULL, model.Id ?? 0, "找不到要更新的常用词");
            if (entity.Title.Trim() != model.Title.Trim())
            {
                var exists = await _phraseRepository.CheckTitleAsync(model.Title, model.CatalogueId);
                if (exists) return new ResponseBase<int>(EStatusCode.CExist, 0, "同一个目录不能出现同样的常用词标题");
            }
            entity.Update(model.Title, model.Text, model.Sort);
            var res = await _phraseRepository.ModifyAsync(entity);
            return new ResponseBase<int>(EStatusCode.COK, res.Id);
        }

        /// <summary>
        /// 删除常用词
        /// </summary>
        /// <param name="ids">指定的常用词Id集合</param>
        /// <param name="role">期望展示的权限 -1=拒绝，没有任何权限  , 0=管理员 , 1=医生 , 2=护士</param>
        /// <returns></returns> 
        [UnitOfWork]
        public async Task<ResponseBase<bool>> DeletePhraseAsync(int[] ids, EExpectedRole role)
        {
            var data = await _phraseRepository.CheckPhrasesAsync(ids); 
            var templateType = data.GroupBy(g=>g.TemplateType);
            var keys = templateType.Select(s => s.Key).ToList();

            //如果是管理员，那不能操作个人的数据
            if (role == EExpectedRole.Admin)
            {
                var exists = keys.Any(w => w == ETemplateType.Personal);
                if (exists) return new ResponseBase<bool>(EStatusCode.C401, false, "管理员不能删除个人目录下的常用词");
            }

            //如果是医生，护士等非管理员权限的，只能操作个人的
            if ((int)role > 0)
            {
                var exists = keys.Any(w => (w == ETemplateType.Department) || w == ETemplateType.General);
                if (exists) return new ResponseBase<bool>(EStatusCode.C401, false, "个人不能删除科室或者全院目录下的常用词");
            }

            await _phraseRepository.DeleteAsync(ids);
            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        /// <summary>
        /// 是否有权限操作
        /// </summary>
        /// <returns></returns>
        private bool HasPermission(EExpectedRole role , ETemplateType templateType)
        {
            if ((int)role > 0 && templateType == ETemplateType.Personal) return true;
            if (role == EExpectedRole.Admin && (templateType == ETemplateType.General || templateType == ETemplateType.Department)) return true;
            return false;
        }

    }
}
