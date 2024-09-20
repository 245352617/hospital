using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SamJan.MicroService.PreHospital.Core.BaseEntities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 告知单患者
    /// </summary>
    public class InformPatInfo : BaseEntity<Guid>
    {
        public InformPatInfo SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        public Guid PatientInfoId { get; set; }

        /// <summary>
        /// 送往医院
        /// </summary>
        [Description("送往医院")]
        [StringLength(200)]
        public string SendToHospital { get; set; }

        /// <summary>
        /// 告知患者来源
        /// </summary>
        [Description("告知患者来源")]
        [StringLength(50)]
        public string Source { get; set; }

        /// <summary>
        /// 预警级别
        /// </summary>
        [Description("预警级别")]
        [StringLength(50)]
        public string WarningLv { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Description("车牌号")]
        [StringLength(50)]
        public string CarNum { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [Description("患者姓名")]
        [StringLength(50)]
        public string PatientName { get; set; }

        /// <summary>
        /// 绿通名称
        /// </summary>

        [Description("绿通名称")]
        [StringLength(50)] 
        public string GreenRoadName { get; set; }

        /// <summary>
        /// 电话判断(调度判断)
        /// </summary>
        [Description("电话判断(调度判断)")]
        [StringLength(50)]
        public string Narration { get; set; }

        /// <summary>
        /// 呼救原因(主诉)
        /// </summary>
        [Description("呼救原因(主诉)")]
        [StringLength(50)]
        public string HelpCauseName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Description("性别")]
        [StringLength(50)]
        public string Gender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [Description("年龄")]
        [StringLength(50)]
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
        [Description("准备内容")]
        [StringLength(200)]
        public string PreparationContent { get; set; }

        /// <summary>
        /// 会诊科室
        /// </summary>
        [Description("会诊科室")]
        [StringLength(200)]
        public string ConsultationDept { get; set; }

        /// <summary>
        /// 车辆编码
        /// </summary>
        [Description("车辆编码")]
        [StringLength(60)]
        public string CarCard { get; set; }

        /// <summary>
        /// 病种判断
        /// </summary>
        [Description("病种判断")]
        [StringLength(200)]
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
        [Description("医生")]
        [StringLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 护士
        /// </summary>
        [Description("护士")]
        [StringLength(50)]
        public string NurseName { get; set; }

        /// <summary>
        /// 现场地址
        /// </summary>
        [Description("现场地址")]
        [StringLength(200)]
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