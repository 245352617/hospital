using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 护理单
    /// </summary>
    public class NursingDocumentDto : EntityDto<Guid>
    {
        /// <summary>
        /// 患者唯一ID
        /// </summary> 
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 单据标题
        /// </summary> 
        public string Title { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 护理单编码(eg: NS-ED-A009)
        /// </summary> 
        [Required, StringLength(200, ErrorMessage = "护理单编码需在32字内")]
        public string NursingCode { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary> 
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        public string Patient { get; set; }

        /// <summary>
        /// 性别
        /// </summary> 
        public string Gender { get; set; }

        /// <summary>
        /// 入科当时的年龄
        /// </summary> 
        [StringLength(10)]
        public string Age { get; set; }

        /// <summary>
        /// 身份证
        /// </summary> 
        public string IDCard { get; set; }

        /// <summary>
        /// 床号
        /// </summary> 
        public string BedNumber { get; set; }

        /// <summary>
        /// 入院时间
        /// </summary> 
        [JsonConverter(typeof(DatetimeJsonConvert))]
        public DateTime AdmissionTime { get; set; }

        /// <summary>
        /// 诊断
        /// </summary> 
        public string Diagnose { get; set; }

        /// <summary>
        /// 科室编号
        /// </summary> 
        [StringLength(32, ErrorMessage = "科室编号描述需在32字内")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 出生日期，如果没有身份证需要手工填写
        /// </summary> 
        public DateTime? DayOfBirth { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary> 
        [StringLength(50, ErrorMessage = "科室名称描述需在50字内")]
        public string DeptName { get; set; }

        /// <summary>
        /// 入抢救室的方式【eg: 步行,扶行,抱入,轮椅,平车,救护车】 （The way into the emergency room）
        /// </summary> 
        [StringLength(20, ErrorMessage = "入抢救室的方式20字内")]
        public string EmergencyWay { get; set; }

        /// <summary>
        /// 入量总量
        /// </summary> 
        public string InIntakesTotal { get; set; }

        /// <summary>
        /// 出量总量
        /// </summary> 
        public string OutIntakesTotal { get; set; }

        /// <summary>
        /// 病危
        /// </summary>
        public bool? IsCriticallyIll { get; set; }

        /// <summary>
        /// 病重
        /// </summary>
        public bool? IsSeriouslyIll { get; set; }

        /// <summary>
        /// 病危病重时间
        /// </summary>
        public DateTime? SeriouslyTime { get; set; }

        /// <summary>
        /// 绿通时间
        /// </summary>
        public DateTime? GreenTime { get; set; }

        /// <summary>
        /// 绿色通道
        /// </summary>
        public bool IsGreen { get; set; }

        /// <summary>
        /// 查房信息
        /// </summary>
        public List<WardRoundDto> WardRounds { get; set; }

        /// <summary>
        /// 动态字段对应的标题描述
        /// </summary>
        public virtual DynamicFieldDto DynamicField { get; set; } = new DynamicFieldDto();

        /// <summary>
        /// 护理单记录
        /// </summary>
        public virtual List<NursingRecordDto> NursingRecords { get; set; } = new List<NursingRecordDto>();

        /// <summary>
        /// 表单页集合
        /// </summary>
        public List<SheetDto> Sheet { get; set; } = new List<SheetDto>();

        /// <summary>
        /// 病重病危情况列表
        /// </summary>
        public List<CriticalIllnessDto> CriticalIllnessList { get; set; } = new List<CriticalIllnessDto>();

    }
}
