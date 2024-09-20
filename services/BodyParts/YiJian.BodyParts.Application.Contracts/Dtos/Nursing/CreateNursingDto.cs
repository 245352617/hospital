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
    public class CreateNursingDto : EntityDto<Guid>
    {
        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        [Required] 
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        /// <example></example>
        [Required]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        /// <example></example>
        public string ParaName { get; set; }

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
        /// 添加来源（EM：ECMO,BP：血液净化）
        /// </summary>
        public string AddSource { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string ValueType { get; set; }


        /// <summary>
        /// 小数位数
        /// </summary>
        public string DecimalDigits { get; set; }
    }
}
