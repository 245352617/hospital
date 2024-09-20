#region 代码备注
//------------------------------------------------------------------------------
// 创建描述: 
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
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表：文书记录表
    /// </summary>
    public class ReDocumentNursingDto : Entity<Guid>
    {
        public ReDocumentNursingDto() { }

        public ReDocumentNursingDto(Guid id) : base(id) { }

        /// <summary>
        /// 护理时间
        /// </summary>
        [Required]
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string DeviceCode { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [StringLength(20)]
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string BedNum { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        [StringLength(20)]
        [Required]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [StringLength(30)]
        [Required]
        public string ParaValue { get; set; }


        /// <summary>
        /// 参数值文本
        /// </summary>
        [StringLength(80)]
        public string ParaValueString { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>

        public DateTime? RecordTime { get; set; }

        /// <summary>
        /// 状态标志
        /// </summary>
        [CanBeNull]
        [StringLength(2)]
        public string RecordState { get; set; }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string ClientIp { get; set; }

        /// <summary>
        /// 护士编号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string NurseName { get; set; }

        /// <summary>
        /// 添加来源（EC：ECMO,BP：血液净化）
        /// </summary>
        [CanBeNull]
        [StringLength(10)]
        public string AddSource { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        [Required]
        public int ValidState { get; set; }
    }
}
