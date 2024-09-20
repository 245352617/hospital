namespace YiJian.Handover
{
    using Volo.Abp;

    /// <summary>
    /// 护士交班 记录已存在异常
    /// </summary>
    public class NurseHandoverAlreadyExistsException : UserFriendlyException
    {
        /// <summary>
        /// 护士交班 记录已存在异常
        /// </summary>   
        public NurseHandoverAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
        {
            
        }
    }
}