namespace YiJian.Recipe
{
    using Volo.Abp;

    /// <summary>
    /// 分诊患者id 记录已存在异常
    /// </summary>
    public class OperationApplyAlreadyExistsException : UserFriendlyException
    {
        /// <summary>
        /// 分诊患者id 记录已存在异常
        /// </summary>   
        public OperationApplyAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
        {

        }
    }
}