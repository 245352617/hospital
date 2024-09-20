using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamJan.MicroService.PreHospital.Core;
using StackExchange.Redis;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public partial class SmsAppService
    {
        private const string DutyTelephoneRedisKey = AppSettings.TriageService + ":DutyTelephone";
        
        private IRepository<DutyTelephone, Guid> _dutyTelephoneRepository;
        private IRepository<DutyTelephone, Guid> DutyTelephoneRepository => LazyGetRequiredService(ref _dutyTelephoneRepository);

        /// <summary>
        /// 保存值班电话设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("SMS" + PermissionDefinition.Separator + "DutyTelephoneSettings" + PermissionDefinition.Separator +
              PermissionDefinition.Save)]
        public async Task<JsonResult> SaveDutyTelephoneAsync(DutyTelephoneDto dto)
        {
            try
            {
                DutyTelephone settings;
                var db = DutyTelephoneRepository.GetDbContext();
                if (dto.Id == Guid.Empty)
                {
                    settings = new DutyTelephone();
                    settings = dto.BuildAdapter().AdaptToType<DutyTelephone>();
                    var maxSort = await DutyTelephoneRepository.MaxAsync(x => x.Sort);
                    maxSort += 1;
                    settings.Sort = maxSort;
                    db.Entry(settings).State = EntityState.Added;
                }
                else
                {
                    settings = await DutyTelephoneRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    if (settings == null)
                    {
                        _log.LogError("保存标签设置失败，原因：不存在此标签设置");
                        return JsonResult.Fail("不存在此标签设置");
                    }

                    settings.Sort = dto.Sort;
                    settings.Dept = dto.Dept;
                    settings.No = dto.No;
                    settings.Telephone = dto.Telephone;
                    settings.GreenRoads = dto.GreenRoads;
                    settings.IsEnabled = dto.IsEnabled;
                    settings.TagSettingId = dto.TagSettingId;
                    settings.ConcurrencyStamp = dto.ConcurrencyStamp;
                    db.Entry(settings).State = EntityState.Modified;
                }

                if (await db.SaveChangesAsync() > 0)
                {
                    _log.LogInformation("保存标签设置成功");
                    dto.Id = settings.Id;
                    var json = await _redis.HashGetAsync(TagSettingsRedisKey, dto.TagSettingId.ToString());
                    var tagSettings = JsonHelper.DeserializeObject<TagSettingsDto>(json);
                    dto.TagSettingsName = tagSettings.Name;
                    dto.ConcurrencyStamp = settings.ConcurrencyStamp;
                    dto.Sort = settings.Sort;
                    dto.Time = settings.LastModificationTime ?? settings.CreationTime;
                    await _redis.HashSetAsync(DutyTelephoneRedisKey, dto.Id.ToString(),
                        JsonHelper.SerializeObject(dto));
                    return JsonResult.Ok();
                }

                _log.LogError("保存标签设置失败，原因：保存数据失败");
                return JsonResult.Fail(msg: "保存失败，请检查后重试！！！");
            }
            catch (Exception e)
            {
                _log.LogError("保存标签设置错误，原因：{Msg}", e);
                return JsonResult.Fail(e.Message);
            }
        }


        /// <summary>
        /// 查询值班电话设置列表
        /// </summary>
        /// <param name="searchText">手机编号/手机号码 模糊查询</param>
        /// <param name="dept">科室编码</param>
        /// <param name="tagName">标签名</param>
        /// <returns></returns>
        [Auth("SMS" + PermissionDefinition.Separator + "DutyTelephoneSettings" + PermissionDefinition.Separator +
              PermissionDefinition.List)]
        public async Task<JsonResult<List<DutyTelephoneDto>>> GetDutyTelephoneListAsync(string searchText,string dept,string tagName)
        {
            try
            {
                List<DutyTelephoneDto> res;
                if (!await _redis.KeyExistsAsync(DutyTelephoneRedisKey))
                {
                    var dutyTelephones = await DutyTelephoneRepository.ToListAsync();
                    res = dutyTelephones.Adapt<List<DutyTelephone>, List<DutyTelephoneDto>>();
                    HashEntry[] entries = new HashEntry[res.Count];
                    var index = 0;
                    foreach (var dto in res)
                    {
                        entries[index] = new HashEntry(dto.Id.ToString(), JsonHelper.SerializeObject(dto));
                        index++;
                    }

                    await _redis.HashSetAsync(DutyTelephoneRedisKey, entries);
                }
                else
                {
                    var values = await _redis.HashValuesAsync(DutyTelephoneRedisKey);
                    var json = "[" + string.Join(",", values) + "]";
                    res = JsonHelper.DeserializeObject<List<DutyTelephoneDto>>(json);
                }

                var tagSettingsJr = await GetTagSettingsListAsync("");
                foreach (var dto in res)
                {
                    var tagSettings = tagSettingsJr.Data?.FirstOrDefault(x => x.Id == dto.TagSettingId);
                    if (tagSettings != null)
                    {
                        dto.TagSettingsName = tagSettings.Name;
                    }
                }

                res = res.WhereIf(!searchText.IsNullOrWhiteSpace(),
                        x => x.Telephone.Contains(searchText) || x.No.Contains(searchText))
                    .WhereIf(!dept.IsNullOrWhiteSpace(), x => x.Code == dept)
                    .WhereIf(!tagName.IsNullOrWhiteSpace(), x => x.TagSettingsName == tagName)
                    .OrderByDescending(o => o.Time)
                    .ToList();

                _log.LogInformation("查询标签设置列表成功");
                return JsonResult<List<DutyTelephoneDto>>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("查询标签设置列表错误，原因：{Msg}", e);
                return JsonResult<List<DutyTelephoneDto>>.Fail(e.Message);
            }
        }


        /// <summary>
        /// 删除值班电话
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> DelDutyTelephoneAsync(Guid id)
        {
            try
            {
                var phone = await DutyTelephoneRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (phone == null)
                {
                    _log.LogError("删除值班电话失败，原因：不存在此值班电话");
                    return JsonResult.Fail("不存在此值班电话！！！");
                }

                var db = DutyTelephoneRepository.GetDbContext();
                db.Entry(phone).State = EntityState.Deleted;
                if (await db.SaveChangesAsync() > 0)
                {
                    _log.LogInformation("删除值班电话成功");
                    await _redis.HashDeleteAsync(DutyTelephoneRedisKey, id.ToString());
                    return JsonResult.Ok();
                }
                
                _log.LogError("删除值班电话错误，原因：数据库删除数据失败");
                return JsonResult.Fail("删除失败，请稍后重试！！！");
            }
            catch (Exception e)
            {
                _log.LogError("删除值班电话错误，原因：{Msg}",e);
                return JsonResult.Fail(e.Message);
            }
        }
    }
}