using BeetleX.Http.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Responses;

namespace YiJian.EMR.HospitalClients;

/// <summary>
/// Patient远程接口
/// </summary>
[JsonFormater]
public interface PatientRemoteService
{
    /// <summary>
    ///  更新诊断被电子病历引用的标记
    /// </summary>
    /// <returns></returns>  
    [JsonFormater]
    [Post(Route = "api/patientService/diagnoseRecord/modifyDiagnoseRecordEmrUsed")]
    public Task<ResponseBase<bool>> ModifyDiagnoseRecordEmrUsedAsync(IList<int> pdid);

}



