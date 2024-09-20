namespace YiJian.Handover
{
    using Volo.Abp;

    public class DoctorPatientStatisticAlreadyExistsException : UserFriendlyException
    {
        public DoctorPatientStatisticAlreadyExistsException(string name) : base($"{name}对应记录已存在！")
        {

        }
    }
}