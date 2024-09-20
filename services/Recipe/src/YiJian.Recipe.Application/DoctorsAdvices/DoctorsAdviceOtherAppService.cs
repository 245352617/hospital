using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.DoctorsAdvices.Dto;
using YiJian.DoctorsAdvices.Entities;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.Hospitals.Enums;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.DoctorsAdvices;

/// <summary>
/// 医嘱其他非核心功能模块
/// </summary>
public partial class DoctorsAdviceAppService
{
    /// <summary>
    /// 会诊和交接班以及患者360使用使用
    /// </summary>
    /// <param name="pIId">会诊和交接班必传</param>
    /// <param name="endTime"></param>
    /// <param name="prescribeTypeCode"></param>
    /// <param name="patientId">患者360必传</param>
    /// <param name="filter"></param>
    /// <param name="startTime"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<List<DoctorsAdviceShowListDto>> GetDoctorsAdviceShowListAsync(Guid pIId,
        string patientId,
        string filter,
        DateTime? startTime,
        DateTime? endTime,
        string prescribeTypeCode, int status = -1)
    {
        var query = from d in (await _doctorsAdviceRepository.GetQueryableAsync())
                .WhereIf(pIId != Guid.Empty, x => x.PIID == pIId && (x.Status == ERecipeStatus.Submitted ||
                                                                     x.Status == ERecipeStatus.Confirmed ||
                                                                     x.Status == ERecipeStatus.Executed ||
                                                                     x.Status == ERecipeStatus.Stopped))
                .WhereIf(!AbpStringExtensions.IsNullOrWhiteSpace(patientId),
                    x => x.PatientId == patientId && x.Status != ERecipeStatus.Saved)
                .WhereIf(!AbpStringExtensions.IsNullOrWhiteSpace(filter), x => x.Name.Contains(filter))
                .WhereIf(startTime != null, x => x.ApplyTime >= startTime.Value)
                .WhereIf(endTime != null, x => x.ApplyTime <= endTime.Value)
                .WhereIf(status != -1, x => (int)x.Status == status)
                .WhereIf(!prescribeTypeCode.IsNullOrEmpty(), w => w.PrescribeTypeCode == prescribeTypeCode.Trim())
                    join pr in (await _prescribeRepository.GetQueryableAsync())
                        on d.Id equals pr.DoctorsAdviceId
                        into temp
                    from p in temp.DefaultIfEmpty()
                    join t in (await _treatRepository.GetQueryableAsync())
                        on d.Id equals t.DoctorsAdviceId
                        into temp2
                    from t2 in temp2.DefaultIfEmpty()
                    join pa in (await _pacsRepository.GetQueryableAsync())
                        on d.Id equals pa.DoctorsAdviceId
                        into temp3
                    from t3 in temp3.DefaultIfEmpty()
                    join li in (await _lisRepository.GetQueryableAsync())
                        on d.Id equals li.DoctorsAdviceId
                        into temp4
                    from t4 in temp4.DefaultIfEmpty()
                    orderby d.ApplyTime descending, d.RecipeNo ascending
                    select new DoctorsAdviceShowListDto
                    {
                        Name = d.Name,
                        CategoryName = d.CategoryName,
                        UsageName = p != null ? p.UsageName : "",
                        QtyPerTimes = p != null ? p.QtyPerTimes : 0,
                        DosageQty = p != null ? p.DosageQty.ToString() : "",
                        DosageUnit = p != null ? p.DosageUnit : "",
                        LongDays = p != null ? p.LongDays : 0,
                        Specification = d.ItemType == EDoctorsAdviceItemType.Prescribe && p != null
                            ? p.Specification
                            : d.ItemType == EDoctorsAdviceItemType.Treat && t2 != null
                                ? t2.Specification
                                : "", //药品 诊疗都有
                        FrequencyName = p != null ? p.FrequencyName : "",
                        ApplyTime = d != null ? d.ApplyTime : null,
                        ApplyDoctorName = d != null ? d.ApplyDoctorName : null,
                        StopDateTime = d != null ? d.StopDateTime : null,
                        StopDoctorName = d != null ? d.StopDoctorName : null,
                        PrescribeTypeName = d != null ? d.PrescribeTypeName : null,
                        Status = d != null ? (int)d.Status : -1,
                        RecipeNo = d != null ? d.RecipeNo : null,
                        RecipeGroupNo = d != null ? d.RecipeGroupNo : 1,
                        Price = d != null ? d.Price : 0,
                        Amount = d != null ? d.Amount : 1
                    };
        var list = await query.ToListAsync();
        return list;
    }

    /// <summary>
    /// 获取患者历史医嘱处方信息
    /// </summary>
    /// <param name="patientId">患者id必传</param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public async Task<List<HistoryDoctorsAdvicesDto>> GetDoctorsAdviceHistoryListAsync(string patientId,
        DateTime? startTime,
        DateTime? endTime)
    {
        var query = from md in (await _medDetailResultRepository.GetQueryableAsync())
                    .Where(p => p.PatientId == patientId)
                    join pr in (await _prescriptionRepository.GetQueryableAsync())
                        on md.ChannelNumber equals pr.MyPrescriptionNo
                        into temp2
                    from prespt in temp2.DefaultIfEmpty()
                    join da in (await _doctorsAdviceRepository.GetQueryableAsync())
                    .Where(p => (p.Status == ERecipeStatus.Submitted ||
                                p.Status == ERecipeStatus.Confirmed ||
                                p.Status == ERecipeStatus.Stopped ||
                                p.Status == ERecipeStatus.Rejected ||
                                p.Status == ERecipeStatus.Executed ||
                                p.Status == ERecipeStatus.PayOff ||
                                p.Status == ERecipeStatus.ReturnPremium))
                    .Where(p => p.PatientId == patientId)
                    .WhereIf(startTime != null, x => x.ApplyTime >= startTime.Value.Date)
                    .WhereIf(endTime != null, x => x.ApplyTime <= endTime.Value.Date.AddDays(1))
                      on md.ChannelNumber equals da.PrescriptionNo
                      into temp
                    from doct in temp.DefaultIfEmpty()
                    join pr in (await _prescribeRepository.GetQueryableAsync())
                        on doct.Id equals pr.DoctorsAdviceId
                        into temp1
                    from pres in temp1.DefaultIfEmpty()
                    join t in (await _treatRepository.GetQueryableAsync())
                   on doct.Id equals t.DoctorsAdviceId into temp3
                    from t2 in temp3.DefaultIfEmpty()
                    join pa in (await _pacsRepository.GetQueryableAsync()) on doct.Id equals pa.DoctorsAdviceId into temp4
                    from t4 in temp4.DefaultIfEmpty()
                    join li in (await _lisRepository.GetQueryableAsync()) on doct.Id equals li.DoctorsAdviceId into temp5
                    from t5 in temp5.DefaultIfEmpty()
                    join tox in (await _toxicRepository.GetQueryableAsync())
                            on pres.MedicineId equals tox.MedicineId
                           into temp6
                    from toxic in temp6.DefaultIfEmpty()
                    select new ALLHistoryDoctorsAdvices
                    {
                        Id = md.Id,
                        HisNumber = md.HisNumber,
                        CreationTime = md.CreationTime,
                        DeptName = md.DeptName,
                        DoctorName = md.DoctorName,
                        PatientId = md.PatientId,
                        IsPay = prespt.BillState == 0 ? "未缴费" : (prespt.BillState == 1 ? "已缴费" : (prespt.BillState == 2 ? "已执行" : (prespt.BillState == 3 ? "已退费" : "已作废"))),
                        DoctorsAdviceId = doct.Id,
                        Code = doct.Code,
                        Name = doct.Name,
                        Price = doct != null ? doct.Price : 0,
                        //LongDays = pres != null ? pres.LongDays : null,
                        //DosageQty = pres != null ? pres.DosageQty: 0,
                        //DosageUnit = pres != null ? pres.DosageUnit : "",
                        LongDays = pres.LongDays,
                        DosageQty = pres.DosageQty,
                        DosageUnit = pres.DosageUnit,
                        RecieveQty = doct.RecieveQty,
                        RecieveUnit = doct.RecieveUnit,
                        RecipeNo = doct != null ? doct.RecipeNo : "",
                        RecipeGroupNo = doct != null ? doct.RecipeGroupNo : null,
                        UsageName = pres.UsageName,
                        FrequencyName = pres.FrequencyName,
                        FrequencyCode = pres.FrequencyCode,
                        LimitType = pres.LimitType,
                        RestrictedDrugs = pres.RestrictedDrugs,
                        SkinTestResult = pres.SkinTestResult,
                        IsSkinTest = pres.IsSkinTest,
                        SkinTestSignChoseResult = pres.SkinTestSignChoseResult,
                        IsLimited = pres.LimitType == 1,
                        CategoryCode = doct.CategoryCode,
                        AntibioticLevel = toxic != null ? toxic.AntibioticLevel : null,
                        ToxicLevel = toxic != null ? toxic.ToxicLevel : null,
                        PrescriptionPermission = pres.PrescriptionPermission,
                        AdditionalItemsType = t2 != null ? t2.AdditionalItemsType : null,
                        ExecDeptName = doct.ExecDeptName,
                        PositionName = doct.PositionName,
                        CatalogName = t4 != null ? t4.CatalogName : (t5 != null ? t5.CatalogName : string.Empty),
                        PartName = t4 != null ? t4.PartName : string.Empty,
                        SpecimenName = t5 != null ? t5.SpecimenName : string.Empty,
                        ContainerName = t5 != null ? t5.ContainerName : string.Empty,
                        PharmacyCode = pres.PharmacyCode,
                        PharmacyName = pres.PharmacyName
                    };
        var result = new List<HistoryDoctorsAdvicesDto>();
        var list = await query.ToListAsync();
        var groupList = list
            .Where(p => p.AdditionalItemsType == null || p.AdditionalItemsType == EAdditionalItemType.No) //过滤掉附加处置项
            .GroupBy(p => new
            {
                p.Id,
                p.HisNumber,
                p.CategoryCode,
                p.CreationTime,
                p.DeptName,
                p.DoctorName,
                p.PatientId,
                p.IsPay
            });
        foreach (var item in groupList)
        {
            //如果没有医嘱信息，则不存处方信息
            if (item.All(p => !p.DoctorsAdviceId.HasValue))
                continue;
            var prescribeInfoDtos = ObjectMapper.Map<List<ALLHistoryDoctorsAdvices>, List<PrescribeInfoDto>>(item.ToList());
            foreach (var dto in prescribeInfoDtos)
            {
                if (dto != null)
                {
                    dto.DosageQty = dto.DosageQty == 0 ? null : dto.DosageQty;
                    dto.LongDays = dto.LongDays == 0 ? null : dto.LongDays;
                    dto.RecieveQty = dto.RecieveQty == 0 ? null : dto.RecieveQty;
                }
            }

            var prescribeInfoList = prescribeInfoDtos.GroupBy(p => p.DoctorsAdviceId).Select(s => s.FirstOrDefault()).ToList();
            result.Add(new HistoryDoctorsAdvicesDto()
            {
                MedDetailResultDto = new MedDetailResultDto()
                {
                    HisNumber = item.Key.HisNumber,
                    CreationTime = item.Key.CreationTime.Value,
                    DeptName = item.Key.DeptName,
                    DoctorName = item.Key.DoctorName,
                    IsPay = item.Key.IsPay,
                    PatientId = item.Key.PatientId,
                    CategoryCode = item.Key.CategoryCode
                },
                PrescribeInfoDtos = prescribeInfoList
            });
        }
        result = result.OrderByDescending(p => p.MedDetailResultDto.CreationTime).ToList();
        return result;
    }


    /// <summary>
    /// 更新IsCriticalPrescription工具
    /// </summary>
    /// <returns></returns>
    //[AllowAnonymous] 
    public async Task<dynamic> UpdateIsCriticalPrescriptionAsync()
    {
        try
        {
            var list = await _grpcMasterDataClient.GetHisMedicineSampleAsync(new MasterDataService.HisMedicineSampleRequest { EmergencySign = 1 });
            var medicineIds = list.HisMedicine.Select(s => int.Parse(s.InvId)).ToList();

            //迁移普通开嘱的药品的急诊药数据
            var updateEntities = await (await _prescribeRepository.GetQueryableAsync()).Where(w => medicineIds.Contains(w.MedicineId)).ToListAsync();
            updateEntities.ForEach(x => x.IsCriticalPrescription = true);
            await _prescribeRepository.UpdateManyAsync(updateEntities);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"更新IsCriticalPrescription异常： {ex.Message}");
        }

        return false;
    }

    /// <summary>
    /// 获取狂犬疫苗记录信息
    /// </summary>
    /// <param name="patientId"></param>
    /// <returns></returns>
    public async Task<List<ImmunizationRecord>> GetImmunizationRecordAsync(string patientId)
    {
        var ImmunizationRecords = await (await _iImmunizationRecordRepository.GetQueryableAsync()).Where(x => x.PatientId == patientId).OrderBy(x => x.RecordTime).ToListAsync();
        return ImmunizationRecords;
    }

    /// <summary>
    /// 根据患者id获取药品信息
    /// </summary>
    /// <param name="pI_ID"></param>
    /// <returns></returns> 
    public async Task<List<PrescribeDto>> GetPrescribesByPIIDAsync(Guid pI_ID)
    {
        //药品限制（途径是：注射、静滴 类型的）
        var injectionAndgttName = _configuration["InjectionAndgttName"].Split(",").ToList();

        var adviceList = await _doctorsAdviceRepository.GetListAsync(p => p.PIID == pI_ID && p.ItemType == EDoctorsAdviceItemType.Prescribe && (p.Status == ERecipeStatus.Submitted || p.Status == ERecipeStatus.Confirmed || p.Status == ERecipeStatus.Executed || p.Status == ERecipeStatus.PayOff));
        if (adviceList == null) Oh.Error("未能获取医嘱信息");
        var data = new List<PrescribeDto>();
        foreach (var advice in adviceList)
        {
            var fildata = ObjectMapper.Map<DoctorsAdvice, DoctorsAdvicePartialDto>(advice);

            var prescribe = await _prescribeRepository.FirstOrDefaultAsync(w => w.DoctorsAdviceId == advice.Id && injectionAndgttName.Contains(w.UsageName));
            if (prescribe != null)
            {
                var model = ObjectMapper.Map<Prescribe, PrescribeDto>(prescribe);
                model.FillData(fildata);
                data.Add(model);
            }
        }

        var result = new List<PrescribeDto>();
        data.GroupBy(x => x.Code, (x, y) =>
        {
            var totalQty = y.Sum(a => a.DosageQty);
            var res = y.Select(dto =>
            {
                dto.DosageQty = totalQty;
                return dto;
            }).ToList();
            result.Add(res.First());
            return res;
        }).ToList();

        return result;
    }
}