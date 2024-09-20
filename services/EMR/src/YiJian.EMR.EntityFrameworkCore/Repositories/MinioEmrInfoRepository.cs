using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.EntityFrameworkCore;
using YiJian.EMR.Templates.Entities;
using YiJian.EMR.Writes.Contracts;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Repositories;

/// <summary>
/// Minio对象存储采集表
/// </summary>
public class MinioEmrInfoRepository : EfCoreRepository<EMRDbContext, MinioEmrInfo, Guid>, IMinioEmrInfoRepository
{
    /// <summary>
    /// Minio对象存储采集表
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public MinioEmrInfoRepository(IDbContextProvider<EMRDbContext> dbContextProvider) : base(dbContextProvider)
    { 

    } 

    /// <summary>
    /// 批量添加Minio采集到的PDF信息
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task AddAsync(List<MinioEmrInfo> entities)
    {
        var db = await GetDbContextAsync();
        await db.AddRangeAsync(entities); 
    }


    /// <summary>
    /// 采集当天的所有病历记录
    /// </summary>
    /// <returns></returns>
    public async Task<List<AddMinioEmrInfo>> GetEmrDataAsync(bool isAll = false)
    {
        var db = await GetDbContextAsync(); 

        //已经采集过的电子病历PDF信息
        var patientEmrIds = await db.MinioEmrInfos
            .Where(w=> w.CreationTime >= DateTime.Today)
            .Select(s=>s.PatientEmrId)
            .ToListAsync();

        //查找全部病历
        if (isAll)
        {
            return await db.PatientEmrs 
            .Select(s => new AddMinioEmrInfo
            { 
                PatientName = s.PatientName,
                DoctorCode = s.DoctorCode,
                DoctorName = s.DoctorName,
                PI_ID = s.PI_ID,
                EmrTitle = s.EmrTitle,
                PatientNo = s.PatientNo,
                PatientEmrId = s.Id
            }).ToListAsync();
        }

        //查找当天有效的病历
        return await db.PatientEmrs
            .Where(w => w.CreationTime >= DateTime.Today && patientEmrIds.Contains(w.Id))
            .Select(s => new AddMinioEmrInfo
            { 
                PatientName = s.PatientName,
                DoctorCode = s.DoctorCode,
                DoctorName = s.DoctorName,
                PI_ID = s.PI_ID,
                EmrTitle = s.EmrTitle,
                PatientNo = s.PatientNo,
                PatientEmrId = s.Id 
            }).ToListAsync(); 
    }


}
