namespace YiJian.Handover
{
    using Volo.Abp;

    public class DoctorHandoverAlreadyExistsException : UserFriendlyException
    {
        public DoctorHandoverAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
        {

        }
    }
}