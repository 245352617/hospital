using System;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.Health.Report.Statisticses.Models
{
    /// <summary>
    /// 患者查询条件
    /// </summary>
    public class InputAdmissionRecordModel
    {
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? BeginDate { get; set; }

        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? EndDate { get; set; }
        public string VisitNo { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Sex { get; set; }
        public string DeptCode { get; set; }
        public string Diagnose { get; set; }
        public string EmrTitle { get; set; }
        public string DoctorsAdvice { get; set; }

        public string patientInfo_narrationName { get; set; }       //主诉
        public string patientInfo_presentMedicalHistory { get; set; }       //现病史
        public string patientInfo_pastMedicalHistory { get; set; }       //既往史
        public string patientInfo_Physicalexamination { get; set; }       //体格检查
        public string medicalInfo_aidPacs { get; set; }       //辅助检查结果
        public string medicalInfo_treatOpinion { get; set; }       //处理意见
        public string medicalInfo_courseOfDisease { get; set; }       //病程记录
        public string patientInfo_diagnoseName { get; set; }       //病历诊断

        public string patientInfo_keyDiseasesName { get; set; }       //重点病种
        public string patientInfo_allergyHistory { get; set; }       //过敏史
        public string patientInfo_infectiousHistory { get; set; }       //传染病史
        public string patientInfo_idNo { get; set; }       //身份证号
        public string medicalInfo_changesInCondition { get; set; }       //病情变化情况
        public string medicalInfo_situationAfterRescue { get; set; }       //抢救后情况
        public string medicalInfo_obsRoundRemark { get; set; }       //留观病程查房记录
        public string medicalInfo_consultationDept { get; set; }       //会诊科室
        public string medicalInfo_obsSituation { get; set; }       //留观情况
        public string medicalInfo_participateInRescuers { get; set; }       //参与抢救人员
    }


    public class InputAdmissionRecordByPageModel : InputAdmissionRecordModel
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
    }


}
