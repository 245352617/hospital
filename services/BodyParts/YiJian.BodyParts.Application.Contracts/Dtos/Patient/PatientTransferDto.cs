using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 病人交接
    /// </summary>
    public class PatientTransferDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        /// <example></example>
        public string Temp { get; set; }

        /// <summary>
        /// 脉搏
        /// </summary>
        /// <example></example>
        public string Pulse { get; set; }

        /// <summary>
        /// 心率
        /// </summary>
        /// <example></example>
        public string HeartRate { get; set; }

        /// <summary>
        /// RR
        /// </summary>
        /// <example></example>
        public string Breathing { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        /// <example></example>
        public string DiastolicPressure { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        /// <example></example>
        public string SystolicPressure { get; set; }

        /// <summary>
        /// 意识（0：其他，1：清醒，2：昏迷）
        /// </summary>
        /// <example></example>
        public int? Consciousness { get; set; }

        /// <summary>
        /// 意识其他内容
        /// </summary>
        /// <example></example>
        public string ConOther { get; set; }

        /// <summary>
        /// 皮肤（0：其他，1：正常，2：水肿，3：脱水，4：苍白，5：紫绀，6：黄疸，7：出血点，8：皮疹，9：湿冷）
        /// </summary>
        /// <example></example>
        public int? Skin { get; set; }

        /// <summary>
        /// 皮肤其他内容
        /// </summary>
        /// <example></example>
        public string SkinOther { get; set; }

        /// <summary>
        /// 压疮（0：未查，1：无，2：有）
        /// </summary>
        /// <example></example>
        public int? PressureSores { get; set; }

        /// <summary>
        /// 压疮部位
        /// </summary>
        /// <example></example>
        public string PreSoreSite { get; set; }

        /// <summary>
        /// 压疮面积-长
        /// </summary>
        /// <example></example>
        public string PreSoreLength { get; set; }

        /// <summary>
        /// 压疮面积-宽
        /// </summary>
        /// <example></example>
        public string PreSoreWide { get; set; }

        /// <summary>
        /// 压疮分期
        /// </summary>
        /// <example></example>
        public string PreSoreStage { get; set; }

        /// <summary>
        /// CVC
        /// </summary>
        public bool Cvc { get; set; }

        /// <summary>
        /// CVC型号(0：单腔，1：双腔，2：三腔)
        /// </summary>
        public int? CvcModel { get; set; }

        /// <summary>
        /// PICC
        /// </summary>
        public bool Picc { get; set; }

        /// <summary>
        /// PICC长度
        /// </summary>
        public string PiccLength { get; set; }

        /// <summary>
        /// 气切套管
        /// </summary>
        public bool Tracheotomy { get; set; }

        /// <summary>
        /// 气管插管
        /// </summary>
        public bool TracheaCannula { get; set; }

        /// <summary>
        /// 距门齿刻度
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        /// 胃管
        /// </summary>
        public bool Stomach { get; set; }

        /// <summary>
        /// 胃管长度
        /// </summary>
        public string StomachLength { get; set; }

        /// <summary>
        /// 鼻肠管
        /// </summary>
        public bool Nasointestinal { get; set; }

        /// <summary>
        /// 鼻肠管长度
        /// </summary>
        public string NasoLength { get; set; }

        /// <summary>
        /// 胃造瘘管
        /// </summary>
        public bool Gastrostomy { get; set; }

        /// <summary>
        /// 尿管
        /// </summary>
        public bool Urine { get; set; }

        /// <summary>
        /// 引流管
        /// </summary>
        public bool Drainage { get; set; }

        /// <summary>
        /// 引流管部位
        /// </summary>
        public string DrainagePart { get; set; }

        /// <summary>
        /// 管道其他
        /// </summary>
        public bool CannulaOther { get; set; }

        /// <summary>
        /// 管道其他内容
        /// </summary>
        public string CannulaOtherText { get; set; }

        /// <summary>
        /// 输液情况
        /// </summary>
        public List<Infusion> Infusion { get; set; }

        /// <summary>
        /// 病例
        /// </summary>
        public bool Case { get; set; }

        /// <summary>
        /// 影像资料
        /// </summary>
        public bool ImageReport { get; set; }

        /// <summary>
        /// 术前记录单
        /// </summary>
        public bool PreReport { get; set; }

        /// <summary>
        /// 麻醉单
        /// </summary>
        public bool AnesReport { get; set; }

        /// <summary>
        /// 化验单
        /// </summary>
        public bool TestReport { get; set; }

        /// <summary>
        /// 特护单
        /// </summary>
        public bool CareReport { get; set; }

        /// <summary>
        /// 患者告知书
        /// </summary>
        public bool Notification { get; set; }

        /// <summary>
        /// 健康宣教
        /// </summary>
        public bool HealthEducation { get; set; }

        /// <summary>
        /// 毛巾
        /// </summary>
        public bool Towel { get; set; }

        /// <summary>
        /// 毛巾内容
        /// </summary>
        public string TowelText { get; set; }

        /// <summary>
        /// 纸巾
        /// </summary>
        public bool Tissue { get; set; }

        /// <summary>
        /// 纸巾内容
        /// </summary>
        public string TissueText { get; set; }

        /// <summary>
        /// 病服
        /// </summary>
        public bool Scrubs { get; set; }

        /// <summary>
        /// 胶带
        /// </summary>
        public bool AdhesiveTape { get; set; }

        /// <summary>
        /// 手术名称
        /// </summary>
        /// <example></example>
        public string OperName { get; set; }

        /// <summary>
        /// 手术时间
        /// </summary>
        /// <example></example>
        public DateTime? OperTime { get; set; }

        /// <summary>
        /// 出入科状态（0：入科确认，1：出科确认）
        /// </summary>
        /// <example></example>
        [Required]
        public int CheckState { get; set; }

        /// <summary>
        /// 接收人工号
        /// </summary>
        public string NurseCode { get; set; }
        
        /// <summary>
        /// 接收人姓名
        /// </summary>
        public string NurseName { get; set; }
        
        /// <summary>
        /// 签名时间
        /// </summary>
        public DateTime? InDeptTransferTime { get; set; }
        
        /// <summary>
        /// 科室编号
        /// </summary>
        public string DeptCode { get; set; }
    }

    /// <summary>
    /// 输液情况
    /// </summary>
    public class Infusion 
    { 
        /// <summary>
        /// 药品名称
        /// </summary>
        public string drugName { get; set; }

        /// <summary>
        /// 输液量
        /// </summary>
        public string drugValue { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool edit { get; set; } = false;
    }
}
