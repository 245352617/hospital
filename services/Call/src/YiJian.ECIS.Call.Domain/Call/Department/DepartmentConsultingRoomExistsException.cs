namespace YiJian.ECIS.Call.Domain
{
    using Volo.Abp;
    public class DepartmentConsultingRoomExistsException : BusinessException
    {
        public DepartmentConsultingRoomExistsException() : base(message: "有诊室存在不可删除！")
        {

        }
    }
}
