using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 接诊病人详细视图
    /// </summary> 
    public class ViewAdmissionRecordResponseDto
    {
        /// <summary>
        /// 就诊号
        /// </summary> 
        public string VisitNo { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary> 
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary> 
        public string PatientID { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary> 
        public string SexName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary> 
        public string Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary> 
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 身份证
        /// </summary> 
        public string IDNo { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary> 
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 接诊医生
        /// </summary> 
        public string FirstDoctorCode { get; set; }

        /// <summary>
        /// 接诊医生
        /// </summary> 
        public string FirstDoctorName { get; set; }

        /// <summary>
        /// 接诊护士
        /// </summary> 
        public string DutyNurseCode { get; set; }

        /// <summary>
        /// 接诊护士
        /// </summary> 
        public string DutyNurseName { get; set; }

        /// <summary>
        /// 接诊时间
        /// </summary>  
        public DateTime VisitDate { get; set; }

        /// <summary>
        /// 分诊等级
        /// </summary> 
        public string TriageLevel { get; set; }

        /// <summary>
        /// 分诊等级名称
        /// </summary> 
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary> 
        public DateTime? OutDeptTime { get; set; }

        /// <summary>
        /// 流转类型
        /// </summary> 
        public string TransferType { get; set; }

        /// <summary>
        /// 流转时间
        /// </summary> 
        public DateTime? TransferTime { get; set; }

        /// <summary>
        /// 滞留时长-分钟
        /// </summary> 
        public int ResidenceTime { get; set; }

        /// <summary>
        /// 流转源区域
        /// </summary> 
        public string FromAreaCode { get; set; }

        /// <summary>
        /// 流转目标区域
        /// </summary> 
        public string ToAreaCode { get; set; }

        /// <summary>
        /// 流转目标区域名称
        /// </summary> 
        public string ToArea { get; set; }

        /// <summary>
        /// 流转原因
        /// </summary> 
        public string TransferReason { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [NotMapped]
        public int Row { get; set; }

    }

}
