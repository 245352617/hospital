namespace YiJian.Handover
{
    using Volo.Abp;

    public class DoctorPatientsAlreadyExistsException : UserFriendlyException
    {
        public DoctorPatientsAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
        {

        }
    }
}