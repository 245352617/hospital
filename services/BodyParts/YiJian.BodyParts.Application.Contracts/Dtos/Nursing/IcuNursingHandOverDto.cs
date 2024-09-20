using JetBrains.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:护士交班表
    /// </summary>
    public class IcuNursingHandOverDto : EntityDto<Guid>
    {
        /// <summary>
        /// 交班日期
        /// </summary>
        /// <example></example>
        [Required(ErrorMessage = "交班日期不能为空！")]
        public DateTime HandOverTime { get; set; }

        /// <summary>
        /// 员工编码
        /// </summary>
        /// <example></example>
        [Required(ErrorMessage = "员工编码不能为空！")]
        [StringLength(20, ErrorMessage = "员工工号长度不能超过20")]
        public string StaffCode { get; set; }

        /// <summary>
        /// 员工名字
        /// </summary>
        /// <example></example>
        [Required(ErrorMessage = "员工名字不能为空！")]
        [StringLength(40, ErrorMessage = "员工名字长度不能超过40")]
        public string StaffName { get; set; }

        /// <summary>
        /// 生命体征信息
        /// </summary>
        /// <example></example>
        [Required(ErrorMessage = "生命体征信息不能为空！")]
        [StringLength(300, ErrorMessage = "生命体征内容不能超过300")]
        public string HealthSign { get; set; }

        /// <summary>
        /// 呼吸监测
        /// </summary>
        [StringLength(300)]
        public string Respiration { get; set; }

        /// <summary>
        /// 待执行医嘱
        /// </summary>
        /// <example></example>
        [CanBeNull] 
        [StringLength(5000)] public string WaitDocSign { get; set; }

        /// <summary>
        /// 出入量记录
        /// </summary>
        /// <example></example>
        [Required(ErrorMessage = "出入量记录不能为空！")]
        [StringLength(300, ErrorMessage = "出入量记录内容不能超300")]
        public string InOutCount { get; set; }

        /// <summary>
        /// 护理导管记录
        /// </summary>
        /// <example></example>
        [Required(ErrorMessage = "导管护理记录不能为空！")]
        [StringLength(5000, ErrorMessage = "导管护理记录长度不能超过5000")]
        public string Canula { get; set; }

        /// <summary>
        /// 特殊交班信息
        /// </summary>
        /// <example></example>
        [CanBeNull] 
        [StringLength(2000)] public string SpcialContent { get; set; }

        /// <summary>
        /// 未填写生命体征
        /// </summary>
        /// <example></example>
        [CanBeNull] 
        [StringLength(300)] public string UnfinshHealthSign { get; set; }

        /// <summary>
        /// 未完成医嘱
        /// </summary>
        /// <example></example>
        [CanBeNull] 
        [StringLength(1000)] public string UnfinishDocAdvice { get; set; }

        /// <summary>
        /// 病人入院流水号
        /// </summary>
        /// <example></example>
        [Required(ErrorMessage = "病人入院流水号不能为空！")]
        [StringLength(20, ErrorMessage = "病人入院流水号长度不能超过40")]
        public string PI_ID { get; set; }

        /// <summary>
        /// 是否为手工修改内容(0为非修改，1为修改过）
        /// </summary>
        /// <example></example>
        [Required] 
        public int IsUpdate { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        /// <example></example>
        [Required(ErrorMessage = "班次内容为空！")]
        [StringLength(20)] 
        public string ScheduleCode { get; set; }

        /// <summary>
        /// 交接班时间
        /// </summary>
        /// <example></example>
        public DateTime? WorkingTime { get; set; }

        /// <summary>
        /// 签名内容
        /// </summary>
        public Guid SignatureId { get; set; }

        /// <summary>
        /// 签名内容
        /// </summary>
        [Required(ErrorMessage = "签名内容为空！")]
        public string Signature { get; set; }
    }
}
