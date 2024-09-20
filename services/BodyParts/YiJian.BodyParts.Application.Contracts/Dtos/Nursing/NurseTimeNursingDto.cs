#region 代码备注
//------------------------------------------------------------------------------
// 创建描述:  10/29/2020 02:40:26
//
// 功能描述:
//
// 修改描述:
//------------------------------------------------------------------------------
#endregion 代码备注
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 病人护理时间和时间对应的护理记录
    /// </summary>
    public class NurseTimeNursingDto : EntityDto<Guid>
    {

        /// <summary>
        /// 护理日期
        /// </summary>
        /// <example></example>
        [Required] public DateTime NurserDate { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        [Required] public DateTime NurseTime { get; set; }

        /// <summary>
        /// 护士工号
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        /// <example></example>
        [CanBeNull] [StringLength(20)] public string NurseName { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required] [StringLength(20)] public string PI_ID { get; set; }

        /// <summary>
        /// 签名表外键
        /// </summary>
        /// <example></example>
        public Guid? SignatureCode { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        public DateTime? RecordTime { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        /// <example></example>
        [Required] public int ValidState { get; set; }

        public List<NursingDto> nursingDto { get; set; }
    }


    /// <summary>
    /// 护理项目
    /// </summary>
    public class NursingDto : EntityDto<Guid>
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModuleCode { get; set; }
        /// <summary>
        /// 护理日期
        /// </summary>
        /// <example></example>
        public DateTime NurseDate { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        /// <example></example>
        public string DeviceCode { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>
        /// <example></example>
        public string BedNum { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        /// <example></example>
        public string ParaValue { get; set; }

        /// <summary>
        /// 参数值串
        /// </summary>
        /// <example></example>
        public string ParaValueString { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        public DateTime? RecordTime { get; set; }

        /// <summary>
        /// 状态标志
        /// </summary>
        /// <example></example>
        public string RecordState { get; set; }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        /// <example></example>
        public string ClientIp { get; set; }

        /// <summary>
        /// 护士编号
        /// </summary>
        /// <example></example>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        /// <example></example>
        public string NurseName { get; set; }

        /// <summary>
        /// 属性参数编号
        /// </summary>
        /// <example></example>
        public string PropertyParaCode { get; set; }

        /// <summary>
        /// 属性值内容
        /// </summary>
        /// <example></example>
        public string PropertyText { get; set; }

        /// <summary>
        /// 颜色参数编号
        /// </summary>
        /// <example></example>
        public string ColorParaCode { get; set; }

        /// <summary>
        /// 颜色值内容
        /// </summary>
        /// <example></example>
        public string ColorText { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <example></example>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 停用时间
        /// </summary>
        /// <example></example>
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 添加次数
        /// </summary>
        public int? AddTimes { get; set; }

        /// <summary>
        /// 添加来源
        /// </summary>
        public string AddSource { get; set; }

        public string CanulaId { get; set; }
        
        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }
    }
}
