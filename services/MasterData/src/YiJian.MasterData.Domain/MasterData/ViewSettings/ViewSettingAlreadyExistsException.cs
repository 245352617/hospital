using Volo.Abp;

namespace YiJian.MasterData.ViewSettings;

/// <summary>
/// 视图配置 记录已存在异常
/// </summary>
public class ViewSettingAlreadyExistsException : UserFriendlyException
{
    /// <summary>
    /// 视图配置 记录已存在异常
    /// </summary>   
    public ViewSettingAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
    {
        
    }
}
