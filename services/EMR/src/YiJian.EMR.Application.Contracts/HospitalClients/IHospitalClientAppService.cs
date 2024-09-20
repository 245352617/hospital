using BeetleX.Http.Clients;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Models.Responses;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.HospitalClients.Dto;
using YiJian.EMR.Templates.Dto;
using YiJian.EMR.Writes.Dto;

namespace YiJian.EMR.HospitalClients;

/// <summary>
/// 医院系统访问
/// </summary>
public interface IHospitalClientAppService
{
     
}

/// <summary>
/// 字典服务
/// </summary>
public interface IMasterDataAppService
{ 
    /// <summary>
    /// 获取电子病历水印配置信息
    /// </summary>
    /// <returns></returns>
    public Task<EmrWatermarkDto> GetEmrWatermarkingAsync();
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



