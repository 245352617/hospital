using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class AreaDto
    {
        /// <summary>
        /// PVID 分诊库患者基本信息表主键ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }


        /// <summary>
        /// 就诊区域编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 就诊区域
        /// </summary>
        public string AreaName { get; set; }


        /// <summary>
        /// 就诊状态：-1=全部、0=未挂号、1=待就诊、2=过号、3=已退号、4=正在就诊、5=已就诊、6=出科
        /// </summary>
        public int VisitStatus { get; set; }
    }
}
