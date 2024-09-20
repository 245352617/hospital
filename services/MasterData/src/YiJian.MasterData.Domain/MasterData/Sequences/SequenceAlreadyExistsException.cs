using Volo.Abp;

namespace YiJian.MasterData.Sequences;


/// <summary>
/// 序列 记录已存在异常
/// </summary>
public class SequenceAlreadyExistsException : UserFriendlyException
{
    /// <summary>
    /// 序列 记录已存在异常
    /// </summary>   
    public SequenceAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
    {
        
    }
}