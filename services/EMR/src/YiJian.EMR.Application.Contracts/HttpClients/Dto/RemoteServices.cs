using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.EMR.HttpClients.Dto;

/// <summary>
/// 远程服务配置BaseAddress
/// </summary>
public class RemoteServices
{
    /// <summary>
    /// 字典服务BaseAddress
    /// </summary>
    public BaseAddress Default { get; set; }

    /// <summary>
    /// 患者服务BaseAddress
    /// </summary>
    public PatientBaseAddress Patient { get; set; }

    public RecipeBaseAddress Recipe { get;set;}

    /// <summary>
    /// 医院的服务BaseAddress
    /// </summary>
    public BaseAddress Hospital { get; set; }
    /// <summary>
    /// 云签BaseAddress
    /// </summary>
    public BaseAddress StampBase { get; set; }
    /// <summary>
    /// 认证中心 BaseAddress
    /// </summary>
    public IEnumerable<BaseAddress> Identity { get; set; }

    public bool LDC { get; set; }

}

public class BaseAddress
{
    public string BaseUrl { get; set; }
}

/// <summary>
/// 患者服务配置地址
/// </summary>
public class PatientBaseAddress: BaseAddress
{
    /// <summary>
    /// 更新诊断记录被电子病历使用的操作
    /// </summary>
    public string ModifyDiagnoseRecordEmrUsed { get; set; }
}

/// <summary>
/// 医嘱服务
/// </summary>
public class RecipeBaseAddress : BaseAddress
{
    /// <summary>
    /// 已打印则将所有的未设为导入的设为导入，方便下次导入不再重复
    /// </summary>
    public string Printed { get; set; }
}
