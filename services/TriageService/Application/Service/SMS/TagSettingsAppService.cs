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
        private const string TagSettingsRedisKey = AppSettings.TriageService + ":TagSettings";

        private IRepository<TagSettings, Guid> _tagSettingsRepository;
        private IRepository<TagSettings, Guid> TagSettingsRepository => LazyGetRequiredService(ref _tagSettingsRepository);


        /// <summary>
        /// 保存标签设置
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Auth("SMS" + PermissionDefinition.Separator + "TagSettings" + PermissionDefinition.Separator +
              PermissionDefinition.Save)]
        public async Task<JsonResult> SaveTagSettingsAsync(TagSettingsDto dto)
        {
            try
            {
                TagSettings settings;
                var db = TagSettingsRepository.GetDbContext();
                var alreadySave = await TagSettingsRepository.Where(x => x.Id == dto.Id || x.Name == dto.Name)
                        .Select(s => new 
                        {
                            s.Id,
                            s.Name
                        })
                        .FirstOrDefaultAsync();
                if (alreadySave != null && !alreadySave.Name.IsNullOrWhiteSpace() && alreadySave.Name == dto.Name)
                {
                    _log.LogError("保存标签设置失败，原因：标签{Name}已经添加了！！！",dto.Name);
                    return JsonResult.Fail($"标签{dto.Name}已经添加了，请勿重复添加！！！");
                }
                
                if (dto.Id == Guid.Empty && (alreadySave == null || alreadySave.Id == Guid.Empty))
                {
                    settings = dto.BuildAdapter().AdaptToType<TagSettings>();
                    db.Entry(settings).State = EntityState.Added;
                }
                else
                {
                    settings = await TagSettingsRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
                    if (settings == null)
                    {
                        _log.LogError("保存标签设置失败，原因：不存在此标签设置");
                        return JsonResult.Fail("不存在此标签设置！！！");
                    }

                    settings.Name = dto.Name;
                    settings.IsSendMessage = dto.IsSendMessage;
                    db.Entry(settings).State = EntityState.Modified;
                }

                if (await db.SaveChangesAsync() > 0)
                {
                    _log.LogInformation("保存标签设置成功！！！");
                    dto.Id = settings.Id;
                    await _redis.HashSetAsync(TagSettingsRedisKey, dto.Id.ToString(), JsonHelper.SerializeObject(dto));
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
        /// 查询标签设置列表
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [Auth("SMS" + PermissionDefinition.Separator + "TagSettings" + PermissionDefinition.Separator +
              PermissionDefinition.List)]
        public async Task<JsonResult<List<TagSettingsDto>>> GetTagSettingsListAsync(string name)
        {
            try
            {
                List<TagSettingsDto> res;
                if (!await _redis.KeyExistsAsync(TagSettingsRedisKey))
                {
                    var settingsList = await TagSettingsRepository.ToListAsync();
                    res = settingsList.Adapt<List<TagSettings>, List<TagSettingsDto>>();
                    HashEntry[] entries = new HashEntry[res.Count];
                    var index = 0;
                    foreach (var dto in res)
                    {
                        entries[index] = new HashEntry(dto.Id.ToString(), JsonHelper.SerializeObject(dto));
                        index++;
                    }

                    await _redis.HashSetAsync(TagSettingsRedisKey, entries);
                }
                else
                {
                    var values = await _redis.HashValuesAsync(TagSettingsRedisKey);
                    var json = "[" + string.Join(",", values) + "]";
                    res = JsonHelper.DeserializeObject<List<TagSettingsDto>>(json);
                }

                res = res.WhereIf(!name.IsNullOrWhiteSpace(), x => x.Name.Contains(name))
                    .ToList();
                _log.LogInformation("查询标签设置列表成功");
                return JsonResult<List<TagSettingsDto>>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.LogError("查询标签设置列表错误，原因：{Msg}", e);
                return JsonResult<List<TagSettingsDto>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 删除标签设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> DelTagSettingsAsync(Guid id)
        {
            try
            {
                var settings = await TagSettingsRepository.FirstOrDefaultAsync(x => x.Id == id);
                if (settings == null)
                {
                    _log.LogError("删除标签设置失败，原因：不存在此标签设置");
                    return JsonResult.Fail("不存在标签设置！！！");
                }

                var isUse = await (from a in TagSettingsRepository
                        join b in DutyTelephoneRepository on a.Id equals b.TagSettingId
                        select new
                        {
                            a.Id
                        })
                    .AnyAsync(x=>x.Id == id);
                if (isUse)
                {
                    _log.LogError("删除标签设置失败，原因：此标签正在使用，不能删除");
                    return JsonResult.Fail("此标签正在使用，不能删除！！！");
                }

                var db = TagSettingsRepository.GetDbContext();
                db.Entry(settings).State = EntityState.Deleted;
                if (await db.SaveChangesAsync() > 0)
                {
                    _log.LogInformation("删除标签设置成功");
                    await _redis.HashDeleteAsync(TagSettingsRedisKey, id.ToString());
                    return JsonResult.Ok();
                }
                
                _log.LogError("删除标签设置错误，原因：数据库删除数据失败");
                return JsonResult.Fail("删除失败，请稍后重试！！！");
            }
            catch (Exception e)
            {
                _log.LogError("删除标签设置错误，原因：{Msg}",e);
                return JsonResult.Fail(e.Message);
            }
        }
    }
}