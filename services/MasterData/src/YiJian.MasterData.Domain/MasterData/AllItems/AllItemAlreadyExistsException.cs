using Volo.Abp;

namespace YiJian.MasterData.AllItems;

/// <summary>
/// 诊疗检查检验药品项目合集 记录已存在异常
/// </summary>
public class AllItemAlreadyExistsException : UserFriendlyException
{
    /// <summary>
    /// 诊疗检查检验药品项目合集 记录已存在异常
    /// </summary>   
    public AllItemAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
    {
        
    }
}