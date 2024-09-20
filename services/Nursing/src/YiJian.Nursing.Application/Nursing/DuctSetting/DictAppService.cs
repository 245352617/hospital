using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:导管字典-通用业务 API
    /// </summary>
    [NonUnify]
    [Authorize]
    public class DictAppService : NursingAppService, IDictAppService
    {
        private readonly DictManager _dictManager;
        private readonly IDictRepository _dictRepository;

        #region constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dictRepository"></param>
        /// <param name="dictManager"></param>
        public DictAppService(IDictRepository dictRepository, DictManager dictManager)
        {
            _dictRepository = dictRepository;
            _dictManager = dictManager;
        }
        #endregion constructor

        #region Create
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Guid> CreateAsync(DictCreation input)
        {
            var dict = await _dictManager.CreateAsync(paraCode: input.ParaCode,// 参数代码
                paraName: input.ParaName,       // 参数名称
                dictCode: input.DictCode,       // 字典代码
                dictValue: input.DictValue,     // 字典值
                dictDesc: input.DictDesc,       // 字典值说明
                parentId: input.ParentId,       // 上级代码
                dictStandard: input.DictStandard,// 字典标准（国标、自定义）
                hisCode: input.HisCode,         // HIS对照代码
                hisName: input.HisName,         // HIS对照
                deptCode: input.DeptCode,       // 科室代码
                moduleCode: input.ModuleCode,   // 模块代码
                sort: input.Sort,               // 排序
                isDefault: input.IsDefault,     // 是否默认
                isEnable: input.IsEnable      // 是否启用
                );

            return dict.Id;
        }
        #endregion Create

        #region Update
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Guid id, DictUpdate input)
        {
            var dict = await _dictRepository.GetAsync(id);

            dict.Modify(paraName: input.ParaName,// 参数名称
                dictCode: input.DictCode,       // 字典代码
                dictValue: input.DictValue,     // 字典值
                dictDesc: input.DictDesc,       // 字典值说明
                parentId: input.ParentId,       // 上级代码
                dictStandard: input.DictStandard,// 字典标准（国标、自定义）
                hisCode: input.HisCode,         // HIS对照代码
                hisName: input.HisName,         // HIS对照
                deptCode: input.DeptCode,       // 科室代码
                moduleCode: input.ModuleCode,   // 模块代码
                sort: input.Sort,               // 排序
                isDefault: input.IsDefault,     // 是否默认
                isEnable: input.IsEnable       // 是否启用
                );

            await _dictRepository.UpdateAsync(dict);
        }
        #endregion Update

        #region Get
        #region 参数字典项目
        /// <summary>
        /// 根据模块代码、参数代码查询参数字典列表
        /// </summary>
        /// <param name="moduleCode">模块代码</param>
        /// <param name="paraCode">参数代码</param>
        /// <returns></returns>
        public async Task<JsonResult<List<DictData>>> SelectDictListAsync(string moduleCode, string paraCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(moduleCode) || string.IsNullOrWhiteSpace(paraCode))
                {
                    return JsonResult<List<DictData>>.RequestParamsIsNull(msg: "请输入模块代码、参数代码！");
                }

                List<Dict> dicts = (await _dictRepository.GetListAsync(s => s.ModuleCode == moduleCode && s.ParaCode == paraCode))
                    .OrderBy(s => s.Sort).ToList();

                List<DictData> dictDtos = ObjectMapper.Map<List<Dict>, List<DictData>>(dicts);
                return JsonResult<List<DictData>>.Ok(data: dictDtos);
            }
            catch (Exception ex)
            {
                return JsonResult<List<DictData>>.Fail(msg: ex.Message);
            }
        }
        #endregion
        #endregion Get

        #region Delete
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            await _dictRepository.DeleteAsync(id);
        }
        #endregion Delete

        #region GetList
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<DictData>> GetListAsync(
            string filter = null,
            string sorting = null)
        {
            var result = await _dictRepository.GetListAsync(filter, sorting);

            return new ListResultDto<DictData>(
                ObjectMapper.Map<List<Dict>, List<DictData>>(result));
        }
        #endregion GetList

        #region GetPagedList
        /// <summary>
        /// 获取分页记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DictData>> GetPagedListAsync(GetDictPagedInput input)
        {
            var dicts = await _dictRepository.GetPagedListAsync(
                    input.SkipCount,
                    input.Size,
                    input.Filter,
                    input.Sorting);

            var items = ObjectMapper.Map<List<Dict>, List<DictData>>(dicts);

            var totalCount = await _dictRepository.GetCountAsync(input.Filter);

            var result = new PagedResultDto<DictData>(totalCount, items.AsReadOnly());

            return result;
        }
        #endregion GetPagedList

    }
}
