using System.Threading.Tasks;


namespace YiJian.Recipe.Application.Backgrounds.Contracts
{
    /// <summary>
    /// 医嘱状态查询
    /// </summary>
    public interface IHospitalBackground
    {
        public Task QueryMedicalInfo();

    }
}
