using Volo.Abp;

namespace YiJian.MasterData.VitalSign;


/// <summary>
/// 评分项 记录已存在异常
/// </summary>
public class VitalSignExpressionAlreadyExistsException : UserFriendlyException
{
    /// <summary>
    /// 评分项 记录已存在异常
    /// </summary>   
    public VitalSignExpressionAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
    {
        
    }
}