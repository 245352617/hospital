using Volo.Abp;

namespace YiJian.MasterData.Sequences;


/// <summary>
/// 编码 记录不存在异常
/// </summary>
public class SequenceCodeNotExistsException : UserFriendlyException
{
    /// <summary>
    /// 编码 记录不存在异常
    /// </summary>   
    public SequenceCodeNotExistsException(string code) : base($"编码{code}不存在！")
    {

    }
}