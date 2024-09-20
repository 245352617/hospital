using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using YiJian.Nursing.Settings;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:人体图-编号字典 API
    /// </summary>
    [NonUnify]
    [Authorize]
    public class CanulaPartAppService : NursingAppService, ICanulaPartAppService
    {
        private readonly ICanulaPartRepository _canulaPartRepository;
        private readonly IParaModuleRepository _paraModuleRepository;

        #region constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="canulaPartRepository"></param>
        /// <param name="paraModuleRepository"></param>
        public CanulaPartAppService(ICanulaPartRepository canulaPartRepository,
            IParaModuleRepository paraModuleRepository)
        {
            _canulaPartRepository = canulaPartRepository;
            _paraModuleRepository = paraModuleRepository;
        }

        #endregion constructor


        /// <summary>
        /// 新增或编辑部位
        /// </summary>
        /// <param name="canula"></param>
        /// <returns></returns>
        public async Task<JsonResult> CreateCanulaPartInfoAsync(CanulaPartData canula)
        {
            try
            {
                CanulaPart finder = canula.Id == Guid.Empty
                    ? null
                    : await _canulaPartRepository.FindAsync(x => x.Id == canula.Id);
                if (finder != null)
                {
                    finder.Sort = canula.Sort;
                    finder.PartName = canula.PartName;
                    finder.PartNumber = canula.PartNumber;
                    finder.IsEnable = canula.IsEnable;
                    finder.ModuleCode = canula.ModuleCode;
                    await _canulaPartRepository.UpdateAsync(finder);
                }
                else
                {
                    canula.Id = GuidGenerator.Create();
                    CanulaPart canulaPart = ObjectMapper.Map<CanulaPartData, CanulaPart>(canula);
                    await _canulaPartRepository.InsertAsync(canulaPart);
                }

                return JsonResult.Ok();
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(msg: "系统异常，请联系管理员！" + ex.ToString());
            }
        }

        /// <summary>
        /// 获取人体图部位分布列表
        /// </summary>
        /// <param name="bodyType">导管：1，皮肤：2</param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public async Task<JsonResult<List<CanulaPartData>>> CanulaPartListAsync(int bodyType,
            string moduleCode)
        {
            try
            {
                string moduleType = bodyType == 1 ? "CANULA" : "SKIN";
                List<string> modules = (await _paraModuleRepository.GetListAsync(x => x.ModuleType == moduleType)).Select(x => x.ModuleCode).ToList();

                var canulaParts = await (await _canulaPartRepository.GetQueryableAsync())
                    .Where(x => modules.Contains(x.ModuleCode))
                    .WhereIf(!string.IsNullOrEmpty(moduleCode), x => x.ModuleCode == moduleCode)
                    .OrderBy(x => x.Sort).ToListAsync();
                var partDtos = ObjectMapper.Map<List<CanulaPart>, List<CanulaPartData>>(canulaParts);

                return JsonResult<List<CanulaPartData>>.Ok(data: partDtos);
            }
            catch (Exception ex)
            {
                return JsonResult<List<CanulaPartData>>.Fail(msg: $"系统异常，请联系管理员！" + ex.ToString());
            }
        }

        /// <summary>
        /// 删除一条人体图部位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> DeleteNoticeInfoAsync(Guid id)
        {
            try
            {
                var finder = id == Guid.Empty
                    ? null
                    : await _canulaPartRepository.GetAsync(x => x.Id == id);
                if (finder != null)
                {
                    await _canulaPartRepository.DeleteAsync(finder);
                    return JsonResult.Ok();
                }

                return JsonResult.DataNotFound();
            }
            catch (Exception ex)
            {
                return JsonResult.Fail(msg: "系统异常，请联系管理员！" + ex.ToString());
            }
        }
    }
}