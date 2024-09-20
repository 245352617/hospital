using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SamJan.MicroService.PreHospital.Core;
using SamJan.MicroService.TriageService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService.Application.Service
{
    /// <summary>
    /// 挂号模式
    /// </summary>
    [Authorize]
    public class RegisterModeAppService : ApplicationService
    {
        private readonly IRepository<RegisterMode> _registerModeRepository;

        public RegisterModeAppService(IRepository<RegisterMode> registerModeRepository)
        {
            this._registerModeRepository = registerModeRepository;
        }

        /// <summary>
        /// 查看挂号模式列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<IEnumerable<RegisterModeData>>> GetListAsync()
        {
            var list = await this._registerModeRepository.GetListAsync();
            var results = list.BuildAdapter().AdaptToType<IEnumerable<RegisterModeData>>();

            return JsonResult<IEnumerable<RegisterModeData>>.Ok("操作成功", results);
        }

        /// <summary>
        /// 查看当前挂号模式
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult<RegisterModeData>> GetCurrentModeAsync()
        {
            var currentMode = await this._registerModeRepository.GetAsync(x => x.IsActive);
            if (currentMode == null)
            {
                return JsonResult<RegisterModeData>.Fail("暂未启用分诊设置");
            }
            var result = currentMode.BuildAdapter().AdaptToType<RegisterModeData>();

            return JsonResult<RegisterModeData>.Ok("查询成功", result);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<JsonResult<RegisterModeData>> UpdateAsync(Guid id, RegisterUpdateDto dto)
        {
            var item = await this._registerModeRepository.GetAsync(x => x.Id == id);
            if (item == null)
            {
                return JsonResult<RegisterModeData>.Fail("叫号模式不存在, 请检查 id.");
            }
            item.Name = dto.TriageConfigName;
            item.IsActive = dto.IsDisable > 0;
            item.Remark = dto.Remark;
            await this._registerModeRepository.UpdateAsync(item);

            if (dto.IsDisable > 0)
            {// 如果启用一个模式，则自动禁用其他模式
                var others = await this._registerModeRepository.Where(x => x.Id != id).ToListAsync();
                foreach (var other in others)
                {
                    other.IsActive = false;
                    await this._registerModeRepository.UpdateAsync(other);
                }
            }
            var result = item.BuildAdapter().AdaptToType<RegisterModeData>();

            return JsonResult<RegisterModeData>.Ok("修改成功", result);
        }
    }
}
