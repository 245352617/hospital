using System;
using System.ComponentModel;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 告知单患者Dto
    /// </summary>
    public class InformPatDto
    {
        /// <summary>
        /// 送往医院
        /// </summary>
        public string SendToHospital { get; set; }

        /// <summary>
        /// 告知患者来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 预警级别
        /// </summary>
        public string WarningLv { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNum { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 绿通名称
        /// </summary>
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 电话判断(调度判断)
        /// </summary>
        public string Narration { get; set; }

        /// <summary>
        /// 呼救原因(主诉)
        /// </summary>
        public string HelpCauseName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 告知时间
        /// </summary>
        public DateTime? InformTime { get; set; }

        /// <summary>
        /// 预计到达时间
        /// </summary>
        public DateTime? ExpectedTime { get; set; }

        /// <summary>
        /// 准备内容
        /// </summary>
        public string PreparationContent { get; set; }

        /// <summary>
        /// 会诊科室
        /// </summary>
        public string ConsultationDept { get; set; }

        /// <summary>
        /// 车辆编码
        /// </summary>
        public string CarCard { get; set; }

        /// <summary>
        /// 病种判断
        /// </summary>
        public string DiseaseIdentification { get; set; }

        /// <summary>
        /// 随车电话
        /// </summary>
        public string CarPhone { get; set; }

        /// <summary>
        /// 病患电话
        /// </summary>
        public string ContactsPhone { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 护士
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 现场地址
        /// </summary>
        public string SiteAddress { get; set; }

        /// <summary>
        /// 血糖
        /// </summary>
        public float GLU { get; set; }

        /// <summary>
        /// 到达现场
        /// </summary>
        public DateTime? ArriveTime { get; set; }

        /// <summary>
        /// 病人上车
        /// </summary>
        public DateTime? BoardingTime { get; set; }

        /// <summary>
        /// 送达医院时间
        /// </summary>
        public DateTime? ArriveHospitalTime { get; set; }

        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime? SamplingTime { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? UploadTime { get; set; }

        /// <summary>
        /// 报告时间
        /// </summary>
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// Pid
        /// </summary>
        public Guid Pid { get; set; }

        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

    }
}