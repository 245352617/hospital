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
using System.Text;
using Volo.Abp.Application.Dtos;


namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:病人护理时间表
    /// </summary>
    public class NurseTimeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        [Required] public DateTime NurseTime { get; set; }

        /// <summary>
        /// 护士工号
        /// </summary>
        /// <example></example>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        /// <example></example>
        public string NurseName { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

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
        /// 添加来源（EM：ECMO,BP：血液净化)
        /// </summary>
        public string AddSource { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        /// <example></example>
        [Required] public int ValidState { get; set; }

        public string ScheduleCode { get; set; }
    }
}
