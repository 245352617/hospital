using YiJian.DoctorsAdvices.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 疫苗记录信息
    /// </summary> 
    public class ImmunizationRecordDto
    {
        public ImmunizationRecordDto(EAcupunctureManipulation acupunctureManipulation, int times)
        {
            AcupunctureManipulation = acupunctureManipulation;
            Times = times;
        }

        /// <summary>
        /// 针法，0=四针法，1=五针法
        /// </summary> 
        public EAcupunctureManipulation AcupunctureManipulation { get; set; }

        /// <summary>
        /// 接种次数（第一次，第二次，第三次...）
        /// </summary> 
        public int Times { get; set; }

    }
}
