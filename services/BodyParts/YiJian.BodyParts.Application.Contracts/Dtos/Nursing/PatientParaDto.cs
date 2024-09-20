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
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{

    /// <summary>
    /// 表:病人观察项表
    /// </summary>
    public class PatientParaDto : EntityDto<Guid>
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        /// <example></example>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        /// <example></example>
        public string ModuleName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        /// <summary>
        /// 是否需要重新加载页面
        /// </summary>
        /// <example></example>
        public bool IsAgainLoad { get; set; }

        public List<ParaItemDto> paraItemDtoList { get; set; } = new List<ParaItemDto>();
    }

    public class ParaItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        /// <example></example>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 参数编号
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        /// <example></example>
        public string ParaName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 评分代码
        /// </summary>
        public string ScoreCode { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// 文本类型
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public string DecimalDigits { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public decimal? SortNum { get; set; }

        /// <summary>
        /// 是否评分（有：N，无：N)
        /// </summary>
        public bool? GuidFlag { get; set; }

        /// <summary>
        /// 评分指引编号
        /// </summary>
        public string GuidId { get; set; }

        /// <summary>
        /// 关联颜色项目名称
        /// </summary>
        public string ColorParaName { get; set; }

        /// <summary>
        /// 关联颜色项目代码
        /// </summary>
        public string ColorParaCode { get; set; }

        public ColorParaItemDto colorParaItemDto { get; set; } = new ColorParaItemDto();

        /// <summary>
        /// 关联性状项目名称
        /// </summary>
        public string PropertyParaName { get; set; }

        /// <summary>
        /// 关联性状项目代码
        /// </summary>
        public string PropertyParaCode { get; set; }

        public ColorParaItemDto propertyParaItemDto { get; set; } = new ColorParaItemDto();

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        public string CanulaId { get; set; }

        /// <summary>
        /// 知识库代码
        /// </summary>
        public string KBCode { get; set; }

        public int IfSum { get; set; }

        /// <summary>
        /// 判断参数是否可以编辑
        /// </summary>
        public int IfEdit { get; set; }

        /// <summary>
        /// 可修改的通气模式值
        /// </summary>
        public string[] VentilationModeValue { get; set; }

        /// <summary>
        /// 护理项目
        /// </summary>
        public List<NursingDto> nursingDto { get; set; } = new List<NursingDto>();

        /// <summary>
        /// 字典列表
        /// </summary>
        public List<DictDto> dictDtoList { get; set; } = new List<DictDto>();

        /// <summary>
        /// 颜色字典列表
        /// </summary>
        public List<DictDto> dictColorList { get; set; } = new List<DictDto>();

        /// <summary>
        /// 属性字典列表
        /// </summary>
        public List<DictDto> dictPropertyList { get; set; } = new List<DictDto>();
    }
}
