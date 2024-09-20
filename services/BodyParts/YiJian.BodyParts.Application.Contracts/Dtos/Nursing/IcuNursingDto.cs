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
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:观察项记录表
    /// </summary>
    public class IcuNursingDto : EntityDto<Guid>
    {
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
        /// 参数值
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
        /// 颜色参数编号
        /// </summary>
        /// <example></example>
        public string ColorParaCode { get; set; }

        /// <summary>
        /// 颜色内容
        /// </summary>
        /// <example></example>
        public string ColorText { get; set; }

        /// <summary>
        /// 属性参数编号
        /// </summary>
        /// <example></example>
        public string PropertyParaCode { get; set; }

        /// <summary>
        /// 属性内容
        /// </summary>
        /// <example></example>
        public string PropertyText { get; set; }

        /// <summary>
        /// 插管编号
        /// </summary>
        public string CanulaId { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }
    }
}
