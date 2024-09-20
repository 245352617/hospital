namespace YiJian.ECIS.Call.Domain
{
    using Volo.Abp;
    public class DepartmentNameAlreadyExistsException : BusinessException
    {
        public DepartmentNameAlreadyExistsException(string name) : base(message: $"{name}对应诊室固定规则已存在！")
        {

        }
    }
}
