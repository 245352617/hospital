using System.Collections.Generic;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Etos.EMRs;

namespace YiJian.EMR.HospitalClients;

/// <summary>
/// 患者服务
/// </summary>
public interface IPatientAppService
{
    /// <summary>
    /// 更新诊断被电子病历引用的标记
    /// </summary>
    /// <param name="pdid"></param>
    /// <returns></returns>
    public Task<bool> ModifyDiagnoseRecordEmrUsedAsync(IList<int> pdid);
}

/// <summary>
/// 医嘱服务
/// </summary>
public interface IRecipeAppService
{
    /// <summary>
    /// 已打印则将所有的未设为导入的设为导入，方便下次导入不再重复
    /// </summary>
    /// <param name="eto"></param>
    /// <returns></returns>
    public Task<bool> PrintedAsync(PrintedAdviceEto eto);

}


