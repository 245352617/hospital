using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.NursingSettings.Contracts;
using YiJian.Health.Report.NursingSettings.Dto;
using YiJian.Health.Report.NursingSettings.Entities;

namespace YiJian.Health.Report.NursingSettings
{
    /// <summary>
    /// 护理单配置
    /// </summary>
    [Authorize]
    public class NursingSettingAppService : ReportAppService, INursingSettingAppService
    {
        private readonly INursingSettingRepository _nursingSettingRepository;
        private readonly INursingSettingHeaderRepository _nursingSettingHeaderRepository;
        private readonly INursingSettingItemRepository _nursingSettingItemRepository;
        private readonly IIntakeSettingRepository _intakeSettingRepository;

        public NursingSettingAppService(
            INursingSettingRepository nursingSettingRepository,
            IIntakeSettingRepository intakeSettingRepository,
            INursingSettingHeaderRepository nursingSettingHeaderRepository,
            INursingSettingItemRepository nursingSettingItemRepository
        )
        {
            _nursingSettingRepository = nursingSettingRepository;
            _nursingSettingHeaderRepository = nursingSettingHeaderRepository;
            _nursingSettingItemRepository = nursingSettingItemRepository;
            _intakeSettingRepository = intakeSettingRepository;
        }

        /// <summary>
        /// 新增或修改主题分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        //[Authorize(ReportPermissions.NursingSettings.Modify)]
        public async Task<ResponseBase<Guid>> ModifySubjectAsync(ModifySubjectDto model)
        {
            //更新
            if (model.Id.HasValue)
            {
                var entity = await (await _nursingSettingRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id);
                if (entity == null) return new ResponseBase<Guid>(EStatusCode.CNULL);

                entity.Update(model.Category, model.Sort);
                _ = await _nursingSettingRepository.UpdateAsync(entity);
                return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
            }
            else
            {
                var entity = new NursingSetting(GuidGenerator.Create(), model.Category, model.Sort, model.GroupId, model.GroupName);
                _ = await _nursingSettingRepository.InsertAsync(entity);
                return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
            }
        }

        /// <summary>
        /// 删除主题分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        //[Authorize(ReportPermissions.NursingSettings.Delete)]
        public async Task<ResponseBase<Guid>> RemoveSubjectAsync(Guid id)
        {
            var any = await (await _nursingSettingHeaderRepository.GetQueryableAsync()).AnyAsync(w => w.NursingSettingId == id);
            if (any)
            {
                return new ResponseBase<Guid>(EStatusCode.C10000);
            }
            await _nursingSettingRepository.DeleteAsync(id);
            return new ResponseBase<Guid>(EStatusCode.COK, id);
        }

        /// <summary>
        /// 删除未发生业务的表头数据集
        /// </summary>
        /// <param name="ids">还未发生业务的表头Id集</param>
        /// <returns></returns> 
        //[Authorize(ReportPermissions.NursingSettings.Delete)]
        [UnitOfWork]
        public async Task<ResponseBase<bool>> RemoveHeadersAsync(List<Guid> ids)
        {
            var ret = new ResponseBase<bool>(EStatusCode.COK, true);
            bool isWarn = false;

            foreach (var id in ids)
            {
                var any = await (await _nursingSettingItemRepository.GetQueryableAsync()).AnyAsync(w => w.NursingSettingHeaderId == id);
                if (any)
                {
                    isWarn = true;
                    if (!ret.Exten.ContainsKey(id.ToString()))
                    {
                        ret.Exten.Add(id.ToString(), $"数据【{id.ToString()}】已经产生业务，如果需要删除请先删除相关的表单域内容");
                    }
                    break;
                }
                await _nursingSettingHeaderRepository.DeleteAsync(id);
            }
            if (isWarn)
            {
                ret.SetCode(EStatusCode.C10000);
            }
            return ret;
        }

        /// <summary>
        /// 删除未发生业务的表单域数据集
        /// </summary>
        /// <see cref="RemoveItemsDto"/>
        /// <returns></returns> 
        //[Authorize(ReportPermissions.NursingSettings.Delete)]
        [UnitOfWork]
        public async Task<ResponseBase<bool>> RemoveItemsAsync(RemoveItemsDto model)
        {
            var item = await (await _nursingSettingItemRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.NursingSettingItemId);
            if (item == null) return new ResponseBase<bool>(EStatusCode.CNULL, "找不到需要删除的表单域");

            if (model.DeleteChildren)
            {
                var lv2 = await (await _nursingSettingItemRepository.GetQueryableAsync()).Where(w => w.NursingSettingItemId.Value == item.Id).ToListAsync();

                if (lv2.Any())
                {
                    await _nursingSettingItemRepository.DeleteManyAsync(lv2);
                    var lv2ChildrenIds = lv2.Select(s => s.Id).ToList();
                    var lv3 = await (await _nursingSettingItemRepository.GetQueryableAsync()).Where(w => lv2ChildrenIds.Contains(w.NursingSettingItemId.Value)).ToListAsync();
                    if (lv3.Any())
                    {
                        await _nursingSettingItemRepository.DeleteManyAsync(lv3);
                    }
                }
            }
            else
            {
                var any = await (await _nursingSettingItemRepository.GetQueryableAsync()).AnyAsync(w => w.NursingSettingItemId.Value == item.Id);
                if (any)
                {
                    return new ResponseBase<bool>(EStatusCode.C10000);
                }
            }

            await _nursingSettingItemRepository.DeleteAsync(item);

            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        /// <summary>
        /// 获取下一层的所有表单域内容(展开同一层级的内容)
        /// </summary>
        /// <see cref="SearchNursingSettingItemsDto"/>
        /// <returns></returns>
        //[Authorize(ReportPermissions.NursingSettings.List)]
        public async Task<ResponseBase<NursingSettingHeaderItemDto>> ExpandItemListAsync(SearchNursingSettingItemsDto model)
        {
            var data = new NursingSettingHeaderItemDto();

            data.InputTypes = InputTypeData.InpuTypes();

            var list = await (await _nursingSettingItemRepository.GetQueryableAsync())
                .WhereIf(model.NursingSettingItemId.HasValue, w => w.NursingSettingItemId == model.NursingSettingItemId)
                .WhereIf(model.NursingSettingHeaderId.HasValue, w => w.NursingSettingHeaderId == model.NursingSettingHeaderId)
                .OrderByDescending(o => o.Sort)
                .ThenBy(o => o.CreationTime)
                .ToListAsync();
            var map = ObjectMapper.Map<List<NursingSettingItem>, List<NursingSettingItemDto>>(list);
            data.Items = map;

            var header = await (await _nursingSettingHeaderRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.NursingSettingHeaderId);
            if (header != null)
            {
                var headerMap = ObjectMapper.Map<NursingSettingHeader, NursingSettingHeaderBaseDto>(header);
                data.Header = headerMap;
            }

            return new ResponseBase<NursingSettingHeaderItemDto>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 新增或更新选项内容（批量操作,需要层级挂载）
        /// </summary>
        /// <see cref="ModifyNursingItemsDto"/>
        /// <returns></returns> 
        //[Authorize(ReportPermissions.NursingSettings.Modify)]
        [UnitOfWork]
        public async Task<ResponseBase<ModifyHeaderDto>> ModifyItemAsync(ModifyNursingItemsDto model)
        {
            /*
             * 批量操作规则
             * 1. 前段传递过来的结构有header 和items ，header是控制分类表头的内容，items是控制表单域集合的内容
             * 2. 目前有三层结构,操作值支持新增和更新两种操作
             *  a.第一层分为新增(第二层必须是新增，第三层也一定是新增)
             *  b.第一层为更新(第二层可分支为更新和新增)
             *    i. 第二层为新增，第三层也必须是新增
             *    ii.第二层为更新，第三层也分为新增和更新
             * 3.新增可以构建好一起提交
             * 4.更新则需要一层一层提交
             * 5.整个过程是一个完整事务
             */

            //1. 处理 header 的内容 
            var header = await ModifyHeaderAsync(model.Header);

            if (header.Code != EStatusCode.COK)
            {
                return new ResponseBase<ModifyHeaderDto>(header.Code, model.Header, header.Message);
            }

            var nursingSettingHeaderId = header.Data;

            //结束表单域的编辑
            if (model.Items == null || model.Items.Count == 0)
            {
                model.Header.Id = header.Data;
                return new ResponseBase<ModifyHeaderDto>(EStatusCode.COK, model.Header);
            }

            var items = model.Items;

            var updateModels = items.Where(w => w.Id.HasValue).ToList();
            var addModels = items.Where(w => !w.Id.HasValue).ToList();

            var addEntities = new List<NursingSettingItem>();

            //如果第一层是新增，则全新更新 新增s=> { 新增s => {新增s} }
            foreach (var item in addModels)
            {
                var entity = new NursingSettingItem(GuidGenerator.Create(), item.InputType, item.Value, item.Watermark,
                    item.Text, item.HasTextblock, item.TextblockLeft, item.TextblockRight, nursingSettingHeaderId,
                    item.HasNext, 0, item.IsCarryInputBox, sort: item.Sort);
                addEntities.Add(entity);

                //第二层
                if (item.Items == null || item.Items.Count == 0)
                {
                    continue;
                }

                foreach (var lv2item in item.Items)
                {
                    var lv2Entity = new NursingSettingItem(GuidGenerator.Create(), lv2item.InputType, lv2item.Value, lv2item.Watermark,
                        lv2item.Text, lv2item.HasTextblock, lv2item.TextblockLeft, lv2item.TextblockRight, null,
                        lv2item.HasNext, 1, lv2item.IsCarryInputBox, entity.Id, lv2item.Sort);
                    addEntities.Add(lv2Entity);

                    //第三层
                    if (lv2item.Items == null || lv2item.Items.Count == 0)
                    {
                        continue;
                    }

                    foreach (var lv3Item in lv2item.Items)
                    {
                        var lv3Entity = new NursingSettingItem(GuidGenerator.Create(), lv3Item.InputType, lv3Item.Value, lv3Item.Watermark,
                            lv3Item.Text, lv3Item.HasTextblock, lv3Item.TextblockLeft, lv3Item.TextblockRight, null,
                            lv3Item.HasNext, 2, lv3Item.IsCarryInputBox, lv2Entity.Id, lv3Item.Sort);
                        addEntities.Add(lv3Entity);
                    }
                }
            }

            //如果第一层更新，则: 更新s=>{ 新增s=> { 新增s } || 更新s=> { 新增s || 更新s } }
            var updateIds = updateModels.Select(s => s.Id.Value);
            if (updateIds.Any())
            {
                var updateEntities = await (await _nursingSettingItemRepository.GetQueryableAsync()).Where(w => updateIds.Contains(w.Id)).ToListAsync();

                foreach (var entity in updateEntities)
                {
                    var itemModel = updateModels.FirstOrDefault(w => w.Id.Value == entity.Id);
                    if (itemModel == null)
                    {
                        continue;
                    }
                    entity.Update(itemModel.InputType, itemModel.Value, itemModel.Watermark, itemModel.Text,
                        itemModel.HasTextblock, itemModel.TextblockLeft, itemModel.TextblockRight, entity.NursingSettingHeaderId,
                        itemModel.HasNext, 0, itemModel.IsCarryInputBox, sort: itemModel.Sort);

                    if (itemModel.Items == null || itemModel.Items.Count == 0)
                    {
                        continue;
                    }

                    //第二层新增
                    var lv2AddItems = itemModel.Items.Where(w => !w.Id.HasValue).ToList();   //不存在的新增  
                    if (lv2AddItems.Count > 0)
                    {
                        foreach (var item in lv2AddItems)
                        {
                            var lv2NewEntity = new NursingSettingItem(GuidGenerator.Create(), item.InputType, item.Value, item.Watermark,
                                item.Text, item.HasTextblock, item.TextblockLeft, item.TextblockRight, null,
                                item.HasNext, 1, item.IsCarryInputBox, entity.Id, item.Sort);
                            addEntities.Add(lv2NewEntity);

                            if (item.Items == null || item.Items.Count == 0)
                            {
                                continue;
                            }

                            //第三层也是新增
                            foreach (var lv3Item in item.Items)
                            {
                                var lv3NewEntity = new NursingSettingItem(GuidGenerator.Create(), lv3Item.InputType, lv3Item.Value, lv3Item.Watermark,
                                    lv3Item.Text, lv3Item.HasTextblock, lv3Item.TextblockLeft, lv3Item.TextblockRight, null,
                                    lv3Item.HasNext, 2, lv3Item.IsCarryInputBox, lv2NewEntity.Id, lv3Item.Sort);
                                addEntities.Add(lv3NewEntity);
                            }
                        }
                    }

                    //第二层更新
                    var lv2UpdateItems = itemModel.Items.Where(w => w.Id.HasValue).ToList(); //存在的，更新  
                    if (lv2UpdateItems.Count > 0)
                    {
                        var lv2UpdateIds = lv2UpdateItems.Select(s => s.Id.Value);

                        if (lv2UpdateIds.Any())
                        {
                            var lv2UpdateEntities = await (await _nursingSettingItemRepository.GetQueryableAsync()).Where(w => lv2UpdateIds.Contains(w.Id)).ToListAsync();

                            foreach (var item in lv2UpdateEntities)
                            {
                                var lv2ItemModel = lv2UpdateItems.FirstOrDefault(w => w.Id.Value == item.Id);
                                if (lv2ItemModel == null)
                                {
                                    continue;
                                }
                                item.Update(lv2ItemModel.InputType, lv2ItemModel.Value, lv2ItemModel.Watermark, lv2ItemModel.Text,
                                    lv2ItemModel.HasTextblock, lv2ItemModel.TextblockLeft, lv2ItemModel.TextblockRight, null,
                                    lv2ItemModel.HasNext, 1, lv2ItemModel.IsCarryInputBox, entity.Id, lv2ItemModel.Sort);

                                if (lv2ItemModel.Items == null || lv2ItemModel.Items.Count == 0)
                                {
                                    continue;
                                }

                                //第三层新增
                                var lv3AddItems = lv2ItemModel.Items.Where(w => !w.Id.HasValue).ToList();   //不存在的新增  
                                if (lv3AddItems.Count > 0)
                                {
                                    foreach (var lv3Item in lv3AddItems)
                                    {
                                        var lv3NewEntity = new NursingSettingItem(GuidGenerator.Create(), lv3Item.InputType, lv3Item.Value, lv3Item.Watermark,
                                            lv3Item.Text, lv3Item.HasTextblock, lv3Item.TextblockLeft, lv3Item.TextblockRight, null,
                                            lv3Item.HasNext, 2, lv3Item.IsCarryInputBox, item.Id, lv3Item.Sort);
                                        addEntities.Add(lv3NewEntity);
                                    }
                                }

                                //第三层更新
                                var lv3UpdateItems = lv2ItemModel.Items.Where(w => w.Id.HasValue).ToList(); //存在的，更新   
                                if (lv3UpdateItems.Count > 0)
                                {
                                    var lv3UpdateIds = lv3UpdateItems.Select(s => s.Id.Value);

                                    if (lv3UpdateIds.Any())
                                    {
                                        var lv3UpdateEntities = await (await _nursingSettingItemRepository.GetQueryableAsync()).Where(w => lv3UpdateIds.Contains(w.Id)).ToListAsync();

                                        foreach (var lv3item in lv3UpdateEntities)
                                        {
                                            var lv3ItemModel = lv3UpdateItems.FirstOrDefault(w => w.Id.Value == lv3item.Id);
                                            if (lv3ItemModel == null)
                                            {
                                                continue;
                                            }
                                            lv3item.Update(lv3ItemModel.InputType, lv3ItemModel.Value, lv3ItemModel.Watermark, lv3ItemModel.Text,
                                                lv3ItemModel.HasTextblock, lv3ItemModel.TextblockLeft, lv3ItemModel.TextblockRight, null,
                                                lv3ItemModel.HasNext, 2, lv3ItemModel.IsCarryInputBox, item.Id, lv3ItemModel.Sort);
                                        }

                                        if (lv3UpdateEntities.Count > 0)
                                        {
                                            //第三层更新
                                            await _nursingSettingItemRepository.UpdateManyAsync(lv3UpdateEntities);
                                        }
                                    }
                                }
                            }

                            if (lv2UpdateEntities.Count > 0)
                            {
                                //第二层更新
                                await _nursingSettingItemRepository.UpdateManyAsync(lv2UpdateEntities);
                            }
                        }
                    }
                }
                //第一层更新
                await _nursingSettingItemRepository.UpdateManyAsync(updateEntities);
            }

            //新增表单域集合(全部)
            await _nursingSettingItemRepository.InsertManyAsync(addEntities);

            model.Header.Id = header.Data;
            return new ResponseBase<ModifyHeaderDto>(EStatusCode.COK, model.Header);
        }

        #region 新增或更新表头内容

        /// <summary>
        /// 新增或更新表头内容
        /// </summary>
        /// <see cref="ModifyHeaderDto"/>
        /// <returns></returns>  
        private async Task<ResponseBase<Guid>> ModifyHeaderAsync(ModifyHeaderDto model)
        {
            var subject = await (await _nursingSettingRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.NursingSettingId);
            if (subject == null) return new ResponseBase<Guid>(EStatusCode.CNULL);

            //更新
            if (model.Id.HasValue)
            {
                return await UpdateHeaderAsync(model);
            }
            else
            {
                return await AddHeaderAsync(model);
            }
        }

        /// <summary>
        /// 新增表头内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ResponseBase<Guid>> AddHeaderAsync(ModifyHeaderDto model)
        {
            var any = await (await _nursingSettingHeaderRepository.GetQueryableAsync()).AnyAsync(w => w.Header == model.Header.Trim());
            if (any)
            {
                return new ResponseBase<Guid>(EStatusCode.CExist, $"[{model.Header.Trim()}]已经重复存在，请换一组名称");
            }

            var entity = new NursingSettingHeader(GuidGenerator.Create(), model.Header.Trim(), model.Sort, model.NursingSettingId, model.InputType, model.IsCarryInputBox);
            _ = await _nursingSettingHeaderRepository.InsertAsync(entity);
            return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
        }

        /// <summary>
        /// 更新表头内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<ResponseBase<Guid>> UpdateHeaderAsync(ModifyHeaderDto model)
        {
            var entities = await (await _nursingSettingHeaderRepository.GetQueryableAsync()).Where(w => w.Id == model.Id || w.Header.Trim() == model.Header.Trim()).ToListAsync();

            if (entities.Any())
            {
                if (entities.Count > 1)
                {
                    return new ResponseBase<Guid>(EStatusCode.CExist, $"[{model.Header.Trim()}]已经重复存在，请换一个名称");
                }

                var entity = entities.FirstOrDefault(w => w.Id == model.Id);
                if (entity == null)
                {
                    return new ResponseBase<Guid>(EStatusCode.CNULL);
                }

                entity.Update(model.Header.Trim(), model.Sort, model.NursingSettingId, model.InputType, model.IsCarryInputBox);
                _ = await _nursingSettingHeaderRepository.UpdateAsync(entity);
                return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
            }
            else
            {
                return new ResponseBase<Guid>(EStatusCode.CNULL);
            }
        }

        #endregion


        /// <summary>
        /// 根据分组ID获取护理单主题集合
        /// </summary>
        /// <returns></returns> 
        //[Authorize(ReportPermissions.NursingSettings.List)]
        public async Task<ResponseBase<List<NursingSettingDto>>> GetNursingSheetListAsync(string groupId)
        {
            var data = await _nursingSettingRepository.GetNursingSheetListAsync(groupId);
            var map = ObjectMapper.Map<List<NursingSetting>, List<NursingSettingDto>>(data);
            return new ResponseBase<List<NursingSettingDto>>(EStatusCode.COK, map);
        }

        /// <summary>
        /// 获取所有的护理单主题集合
        /// </summary>
        /// <returns></returns> 
        //[Authorize(ReportPermissions.NursingSettings.List)]
        public async Task<ResponseBase<List<IGrouping<string, NursingSettingDto>>>> GetAllNursingSheetListAsync()
        {
            var data = await _nursingSettingRepository.GetAllNursingSheetListAsync();
            var map = ObjectMapper.Map<List<NursingSetting>, List<NursingSettingDto>>(data);
            var groupList = map.GroupBy(p => p.GroupId).ToList();
            return new ResponseBase<List<IGrouping<string, NursingSettingDto>>>(EStatusCode.COK, groupList);
        }

        /// <summary>
        /// 点击动态六项返回的配置内容
        /// </summary>
        /// <param name="headerId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">动态六项对应的表头UUID，如：腹部，出血，循环...</exception>
        //[Authorize(ReportPermissions.NursingSettings.Detail)]
        public async Task<ResponseBase<NursingInputOptionsDto>> GetInputOptionsAsync([Required] Guid headerId)
        {
            var model = await _nursingSettingHeaderRepository.GetSixInputOptionsAsync(headerId);
            var map = ObjectMapper.Map<NursingSettingHeader, NursingInputOptionsDto>(model);
            return new ResponseBase<NursingInputOptionsDto>(EStatusCode.COK, map);
        }


        /// <summary>
        /// 新增或修改出入量配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<Guid>> ModifyIntakeSettingAsync(IntakeSettingDto model)
        {
            //更新
            if (model.Id.HasValue)
            {
                var entity = await (await _intakeSettingRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id);
                if (entity == null) return new ResponseBase<Guid>(EStatusCode.CNULL);
                //入量
                if (model.IntakeType == 0)
                {
                    entity.Update((InputTypeEnum)model.InputType, model.InputMode, model.Unit, model.DefaultInputMode, model.DefaultUnit, model.IsEnabled, model.Sort);
                }
                //出量
                else
                {
                    entity.Update((InputTypeEnum)model.InputType, model.Color, model.Traits, model.Unit, model.DefaultColor, model.DefaultTraits, model.DefaultUnit, model.IsEnabled, model.Sort);
                }
                _ = await _intakeSettingRepository.UpdateAsync(entity);
                return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
            }
            else
            {
                IntakeSetting entity;
                //入量
                if (model.IntakeType == 0)
                {
                    entity = new IntakeSetting(GuidGenerator.Create(), model.IntakeType, model.Code, model.Content, (InputTypeEnum)model.InputType, model.InputMode, model.Unit, model.DefaultInputMode, model.DefaultUnit, model.IsEnabled, model.Sort);
                }
                //出量
                else
                {
                    entity = new IntakeSetting(GuidGenerator.Create(), model.IntakeType, model.Code, model.Content, (InputTypeEnum)model.InputType, model.Color, model.Traits, model.Unit, model.DefaultColor, model.DefaultTraits, model.DefaultUnit, model.IsEnabled, model.Sort);
                }
                _ = await _intakeSettingRepository.InsertAsync(entity);
                return new ResponseBase<Guid>(EStatusCode.COK, entity.Id);
            }
        }

        /// <summary>
        /// 删除出入量配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<Guid>> RemoveIntakeSettingAsync(Guid id)
        {
            var any = await (await _intakeSettingRepository.GetQueryableAsync()).AnyAsync(w => w.Id == id);
            if (!any)
            {
                return new ResponseBase<Guid>(EStatusCode.CNULL);
            }
            await _intakeSettingRepository.DeleteAsync(id);
            return new ResponseBase<Guid>(EStatusCode.COK, id);
        }

        /// <summary>
        /// 批量删除出入量配置
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<bool>> BulkRemoveIntakeSettingAsync(List<Guid> ids)
        {
            if (!ids.Any())
            {
                return new ResponseBase<bool>(EStatusCode.ParameterIsMissing);
            }
            var list = (await _intakeSettingRepository.GetQueryableAsync()).Where(p => ids.Contains(p.Id));
            if (!list.Any())
            {
                return new ResponseBase<bool>(EStatusCode.CNULL);
            }
            await _intakeSettingRepository.DeleteManyAsync(list);
            return new ResponseBase<bool>(EStatusCode.COK, true);
        }

        /// <summary>
        /// 获取的出入量配置集合
        /// </summary>
        /// <param name="intakeType">0=入量，1=出量,不传查所有</param>
        /// <param name="keywords">名称/编码</param>
        /// <returns></returns>
        public async Task<ResponseBase<List<IntakeSettingDto>>> GetIntakeSettingListAsync(int? intakeType, string keywords)
        {
            var data = await _intakeSettingRepository.GetIntakeSettingListAsync(intakeType, null, keywords);
            var map = ObjectMapper.Map<List<IntakeSetting>, List<IntakeSettingDto>>(data);
            return new ResponseBase<List<IntakeSettingDto>>(EStatusCode.COK, map);
        }

        /// <summary>
        /// 获取的出入量配置集合(护理单查询)
        /// </summary>
        /// <param name="intakeType">0=入量，1=出量,不传查所有</param>
        /// <returns></returns>
        public async Task<ResponseBase<List<IntakeSettingDto>>> GetIntakeSettingListByTypeAsync(int? intakeType)
        {
            var data = await _intakeSettingRepository.GetIntakeSettingListAsync(intakeType, true, null);
            var map = ObjectMapper.Map<List<IntakeSetting>, List<IntakeSettingDto>>(data);
            return new ResponseBase<List<IntakeSettingDto>>(EStatusCode.COK, map);
        }

        /// <summary>
        /// 启用/禁用出入量配置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnabled">启用/禁用</param>
        /// <returns></returns> 
        public async Task<ResponseBase<Guid>> EnabledIntakeSettingAsync(Guid id, bool isEnabled)
        {
            var entity = await (await _intakeSettingRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null)
            {
                return new ResponseBase<Guid>(EStatusCode.CNULL);
            }
            entity.IsEnabled = isEnabled;
            await _intakeSettingRepository.UpdateAsync(entity);
            return new ResponseBase<Guid>(EStatusCode.COK, id);
        }

    }
}
