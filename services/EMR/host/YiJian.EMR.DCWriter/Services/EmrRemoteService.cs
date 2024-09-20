using BeetleX.Http.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.DCWriter.Models;

namespace YiJian.EMR.DCWriter.Services;

/// <summary>
/// EMR远程接口
/// </summary>
[JsonFormater] 
public interface EmrRemoteService
{
    /// <summary>
    ///  获取电子病历，护理文书的的id集合
    /// </summary>
    /// <returns></returns>
    [Get(Route = "api/ecis/emr/write/archive-patient-emrs")]
    public Task<List<PatientEmrSampleDto>> GetPatientEmrsAsync();

    /// <summary>
    /// 归档
    /// </summary>
    /// <param name="patientEmrId"></param>
    /// <returns></returns> 
    [FromDataFormater]
    [Post( Route ="api/ecis/emr/write/archive/{patientEmrId}")]
    public Task<string> ArchiveAsync(Guid patientEmrId);

    /// <summary>
    /// 归档
    /// </summary>
    /// <param name="patientEmrId"></param>
    /// <returns></returns> 
    [FromDataFormater]
    [Post(Route = "api/ecis/emr/write/rollback-archive/{patientEmrId}")]
    public Task RollbackArchiveAsync(Guid patientEmrId);
}


/// <summary>
/// EMR远程接口
/// </summary>
[JsonFormater]
public interface MasterRemoteService
{
    /// <summary>
    ///  获取电子病历，护理文书的的id集合
    /// </summary>
    /// <returns></returns>
    [Get(Route = "api/MasterData/dictionaries/emr-watermark")]
    public Task<ResponseBase<EmrWatermarkDto>> GetEmrmarkingAsync();
     
}
