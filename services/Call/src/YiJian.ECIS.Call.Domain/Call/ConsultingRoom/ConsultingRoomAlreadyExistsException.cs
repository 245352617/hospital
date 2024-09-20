namespace YiJian.ECIS.Call.Domain
{
    using Volo.Abp;

    public class ConsultingRoomAlreadyExistsException : BusinessException
    {
        public ConsultingRoomAlreadyExistsException(string name):base("",$"诊室 {name} 已存在！")
        {

        }
    }
}
