using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SamJan.MicroService.PreHospital.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.EntityFrameworkCore;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class GroupInjuryRepository : BaseRepository<PreHospitalTriageDbContext, GroupInjuryInfo, Guid>,
        IGroupInjuryRepository
    {
        private readonly NLogHelper _log;

        public GroupInjuryRepository(IDbContextProvider<PreHospitalTriageDbContext> dbContextProvider, NLogHelper log) : base(dbContextProvider)
        {
            _log = log;
        }

        /// <summary>
        /// 查询群伤事件列表及关联患者
        /// </summary>
        /// <param name="input">输入项</param>
        /// <returns></returns>
        public async Task<JsonResult<PagedResultDto<GroupInjuryOutput>>> GetGroupInjuryPatientListAsync(
            PagedGroupInjuryInput input)
        {
            try
            {
                _log.Trace("【GroupInjuryRepository】【GetGroupInjuryPatientListAsync】【查询群伤事件列表及关联患者开始】");
                var groupList = DbContext.GroupInjuryInfo
                    .WhereIf(input.StartTime < input.EndTime, x => x.HappeningTime <= input.EndTime && x.HappeningTime >= input.StartTime)
                    .WhereIf(!string.IsNullOrEmpty(input.GroupInjuryTypeCode), x => x.GroupInjuryCode == input.GroupInjuryTypeCode);
                var totalCount = await groupList.CountAsync();
                PagedResultDto<GroupInjuryOutput> res;
                if (totalCount > 0)
                {
                    var list = await groupList.OrderByDescending(o=>o.HappeningTime).Skip((input.SkipCount - 1) * input.MaxResultCount)
                        .Take(input.MaxResultCount)
                        .ToListAsync();
                    list = list.OrderByDescending(o => o.HappeningTime).ToList();
                    var dtos = list.BuildAdapter().AdaptToType<List<GroupInjuryOutput>>();
                    if (input.IsIncludePatient)
                    {
                        #region 动态构建Lambda表达式

                        Expression<Func<PatientInfo, bool>> first = x => false;
                        var param = Expression.Parameter(typeof(PatientInfo), "x");
                        Expression body = Expression.Invoke(first, param);
                        var guids = string.Join(',', dtos.Select(s => s.GroupInjuryInfoId).ToList());
                        body = guids.Split(',').Select(item =>
                                (Expression<Func<PatientInfo, bool>>) (x => x.GroupInjuryInfoId == Guid.Parse(item)))
                            .Aggregate(body,
                                (current, expression) =>
                                    Expression.OrElse(current, Expression.Invoke(expression, param)));
                        var lambda = Expression.Lambda<Func<PatientInfo, bool>>(body, param);

                        #endregion

                        #region 查询群伤患者

                        var patients = await DbContext.PatientInfo.Where(lambda).Include(i => i.ConsequenceInfo)
                            .ToListAsync();
                        patients = patients.OrderBy(o => o.PatientName).ToList();
                        var patientDtos = patients.BuildAdapter().AdaptToType<List<GroupInjuryPatientDto>>();
                        
                        #endregion
                        
                        dtos.ForEach(item =>
                        {
                            item.GroupInjuryPatients = patientDtos
                                .Where(x => x.GroupInjuryInfoId == item.GroupInjuryInfoId)
                                .ToList();
                            
                            item.PatientCount = item.GroupInjuryPatients?.Count.ToString();
                            item.StLevelCount = item.GroupInjuryPatients.Count(p => p.TriageLevel == TriageLevel.FirstLv.GetDescriptionByEnum());
                            item.NdLevelCount = item.GroupInjuryPatients.Count(p => p.TriageLevel == TriageLevel.SecondLv.GetDescriptionByEnum());
                            item.RdLevelCount = item.GroupInjuryPatients.Count(p => p.TriageLevel == TriageLevel.ThirdLv.GetDescriptionByEnum());
                            item.ThaLevelCount =
                                item.GroupInjuryPatients.Count(p => p.TriageLevel == TriageLevel.FourthALv.GetDescriptionByEnum());
                            item.ThbLevelCount =
                                item.GroupInjuryPatients.Count(p => p.TriageLevel == TriageLevel.FourthBLv.GetDescriptionByEnum());
                        });
                    }

                    res = new PagedResultDto<GroupInjuryOutput>
                    {
                        TotalCount = totalCount,
                        Items = dtos
                    };
                }
                else
                {
                    res = new PagedResultDto<GroupInjuryOutput>
                    {
                        TotalCount = 0,
                        Items = null
                    };
                }


                _log.Trace("【GroupInjuryRepository】【GetGroupInjuryPatientListAsync】【查询群伤事件列表及关联患者结束】");
                return JsonResult<PagedResultDto<GroupInjuryOutput>>.Ok(data: res);
            }
            catch (Exception e)
            {
                _log.Warning($"【GroupInjuryRepository】【GetGroupInjuryPatientListAsync】【查询群伤事件列表及关联患者错误】【Msg：{e}】");
                return JsonResult<PagedResultDto<GroupInjuryOutput>>.Fail(e.Message);
            }
        }

        /// <summary>
        /// 根据患者分诊Id查询该患者群伤事件
        /// </summary>
        /// <param name="triagePatientInfoId"></param>
        /// <returns></returns>
        public async Task<GroupInjuryAndPatientInfoDto> GetGroupInjuryPatientAsync(Guid triagePatientInfoId)
        {
            try
            {
                _log.Trace("【GroupInjuryRepository】【GetGroupInjuryPatientAsync】【根据患者分诊Id查询该患者群伤事件开始】");
                var res = await (from a in DbContext.GroupInjuryInfo
                    join b in DbContext.PatientInfo on a.Id equals b.GroupInjuryInfoId
                    where b.Id == triagePatientInfoId
                    orderby a.CreationTime descending
                    select new GroupInjuryAndPatientInfoDto
                    {
                        GroupInjuryTypeName = a.GroupInjuryName,
                        Remark = a.Remark,
                        Description = a.Description,
                        HappeningTime = a.HappeningTime,
                        GroupInjuryInfoId = a.Id,
                        PatientName = b.PatientName,
                        Age = b.Age,
                        Birthday = b.Birthday,
                        PatientId = b.PatientId,
                        FinalDiagnosis = "",
                        Sex = b.SexName,
                        TaskInfoId = b.TaskInfoId,
                        TriageDept = b.ConsequenceInfo.TriageDeptName,
                        TriageDirection = b.ConsequenceInfo.TriageTargetName,
                        TriageLevel = b.ConsequenceInfo.ActTriageLevelName,
                        TriagePatientInfoId = b.Id,
                        TriageTime = b.ConsequenceInfo.CreationTime
                    }).FirstOrDefaultAsync();
                _log.Trace("【GroupInjuryRepository】【GetGroupInjuryPatientAsync】【根据患者分诊Id查询该患者群伤事件结束】");
                return res;
            }
            catch (Exception e)
            {
                _log.Warning($"【GroupInjuryRepository】【GetGroupInjuryPatientAsync】【根据患者分诊Id查询该患者群伤事件错误】【Msg：{e}】");
                return null;
            }
        }
    }
}