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
using Microsoft.AspNetCore.Authorization;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Templates.Contracts;
using YiJian.EMR.Enums;
using YiJian.EMR.Libs;
using YiJian.EMR.Templates.Entities;
using Volo.Abp.Users;
using YiJian.EMR.Props.Dto;
using YiJian.EMR.Props.Contracts;
using YiJian.EMR.Props.Entities;
using Microsoft.EntityFrameworkCore;

namespace YiJian.EMR.Props
{
    /// <summary>
    /// 电子病历属性
    /// </summary>
    [Authorize]
    public class CategoryPropertyAppService : EMRAppService, ICategoryPropertyAppService
    {
        private readonly ICategoryPropertyRepository _emrPropertyRepository;
        public CategoryPropertyAppService(
            ICategoryPropertyRepository emrPropertyRepository
        )
        {
            _emrPropertyRepository = emrPropertyRepository;
        }


        #region 获取属性结构

        /// <summary>
        /// 电子病历自定义属性树结构
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<List<CategoryPropertyTreeDto>>> GetTreeAsync()
        {
            var list = await (await _emrPropertyRepository.GetQueryableAsync())
                .Select(s => new CategoryPropertyTreeDto
                {
                    Id = s.Id,
                    Value = s.Value,
                    Label = s.Label,
                    ParentId = s.ParentId
                })
                .ToListAsync();

            var root = list.Where(w => w.ParentId == null).ToList();
            if (root.Count == 0)
            {
                return new ResponseBase<List<CategoryPropertyTreeDto>>(EStatusCode.CNULL);
            }

            foreach (var item in root)
            {
                RecursiveTree(list, item);
            }

            return new ResponseBase<List<CategoryPropertyTreeDto>>(EStatusCode.COK, root);
        }

        /// <summary>
        /// 递归获取树结构
        /// </summary>
        /// <param name="list">所有的目录</param>
        /// <param name="item">需要遍历的节点</param>
        private void RecursiveTree(List<CategoryPropertyTreeDto> list, CategoryPropertyTreeDto item)
        {
            var sub = list.Where(w => w.ParentId == item.Id).ToList();
            if (sub.Count == 0)
            {
                return;
            }
            else
            {
                item.Children.AddRange(sub);

                foreach (var subItem in item.Children)
                {
                    RecursiveTree(list, subItem);
                }
            }
        }

        #endregion

        /// <summary>
        /// 添加一个电子病历属性
        /// </summary>
        /// <see cref="CategoryPropertyDto"/>
        /// <returns></returns>
        public async Task<ResponseBase<Guid>> AddPropertyAsync(CategoryPropertyDto model)
        {
            var find = await (await _emrPropertyRepository.GetQueryableAsync())
                .Where(w => w.Value == model.Value || w.Label == model.Label)
                .ToListAsync();

            if (find.Any())
            {
                var valueCount = find.Count(w => w.Value == model.Value);
                if (valueCount > 0)
                {
                    return new ResponseBase<Guid>(EStatusCode.CFail, Guid.NewGuid(), "属性Value重复，无法创建");
                }

                var labelCount = find.Count(w => w.Label == model.Label);

                if (labelCount > 0)
                {
                    return new ResponseBase<Guid>(EStatusCode.CFail, Guid.NewGuid(), "属性Label重复，无法创建");
                }
            }

            var id = GuidGenerator.Create();
            var lv = 0;
            if (model.ParentId.HasValue)
            {
                var parent = await (await _emrPropertyRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.ParentId.Value);
                lv = parent == null ? 0 : parent.Lv + 1;
            }
            var entity = new CategoryProperty(id, model.Value, model.Label, lv, model.ParentId);

            _ = await _emrPropertyRepository.InsertAsync(entity);
            return new ResponseBase<Guid>(EStatusCode.COK, id);
        }

        /// <summary>
        /// 更新一个电子病历属性
        /// </summary>
        /// <see cref="UpdateEmrPropertyDto"/>
        /// <returns></returns>
        public async Task<ResponseBase<Guid>> UpdatePropertyAsync(UpdateEmrPropertyDto model)
        {
            var entity = await (await _emrPropertyRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id); 
            var find = await (await _emrPropertyRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Label == model.Label);
            if (find != null)
            {
                return new ResponseBase<Guid>(EStatusCode.CFail, Guid.NewGuid(), "属性Label重复，无法创建");
            }

            entity.Update(model.Label);
            _ = await _emrPropertyRepository.UpdateAsync(entity);
            return new ResponseBase<Guid>(EStatusCode.COK, model.Id);
        }

        /// <summary>
        /// 删除指定的属性记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> RemoveAsync(Guid id)
        {
            var entity = await (await _emrPropertyRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
            if (entity == null)
            {
                return new ResponseBase<bool>(EStatusCode.CNULL);
            }
            await _emrPropertyRepository.DeleteAsync(entity);
            return new ResponseBase<bool>(EStatusCode.COK,true);
        }

    }
}
