using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 出科Dto
    /// </summary>
    public class UpdateAdmissionByOutDeptDto
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int AR_ID { get; set; }

        /// <summary>
        /// 补充说明
        /// </summary>
        public string SupplementaryNotes { set; get; }

        /// <summary>
        /// 重点病种
        /// </summary>
        public string KeyDiseasesCode { set; get; }

        /// <summary>
        /// 重点病种名称
        /// </summary>
        public string KeyDiseasesName { set; get; }

        /// <summary>
        /// 死亡时间
        /// </summary>
        public DateTime? DeathTime { get; set; }


        /// <summary>
        /// 出科原因编码
        /// </summary>
        public string OutDeptReason { get; set; }


        /// <summary>
        /// 出科原因名称
        /// </summary>
        public string OutDeptReasonName { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 是否取消绿通
        /// </summary>
        public bool IsCancelGreen { get; set; }
    }
}