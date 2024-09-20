using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 基础档案分组
    /// </summary>
    public class DictSourceGroup
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool ParaValue { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 字典列表
        /// </summary>
        public List<DictCaseDto> dicts { get; set; }
    }

    /// <summary>
    /// 基础档案字典返回
    /// </summary>
    public class DictCaseDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Required(ErrorMessage = "参数名称不能为空！")]
        public string ParaName { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool ParaValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }

    /// <summary>
    /// 基础字典返回
    /// </summary>
    public class DictSourceDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Required(ErrorMessage = "参数名称不能为空！")]
        public string ParaName { get; set; }

        /// <summary>
        /// 途径分组：是否泵入
        /// </summary>
        public bool ParaValue { get; set; }

        /// <summary>
        /// 途径分组：是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }

    /// <summary>
    /// 评分字典分组
    /// </summary>
    public class DictScoreGroup
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 评分项目
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字典列表
        /// </summary>
        public List<DictScoreList> dicts { get; set; }
    }
    /// <summary>
    /// 评分字典列表
    /// </summary>
    public class DictScoreList
    {
        /// <summary>
        /// 参数类型
        /// </summary>
        public string ParaType { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage = "参数代码不能为空！")]
        public string ModuleName { get; set; }
        /// <summary>
        /// 参数代码
        /// </summary>
        [Required(ErrorMessage = "参数代码不能为空！")]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [Required(ErrorMessage = "参数名称不能为空！")]
        public string ParaName { get; set; }
    }

    /// <summary>
    /// 护理工作量统计配置
    /// </summary>
    public class NursingWorkStaticsDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 指标名称,加唯一约束
        /// </summary>
        [Required(ErrorMessage = "指标名称不能为空！")]
        [StringLength(50)]
        public string IndexName { get; set; }

        /// <summary>
        /// 数据来源编码，0：医嘱，1：护理记录，2：系统项目，3：其他
        /// </summary>
        [Required(ErrorMessage = "数据来源不能为空！")]
        [StringLength(50)]
        public string DataSource { get; set; }

        /// <summary>
        /// 数据来源名称
        /// </summary>
        [StringLength(50)]
        public string DataSourceName { get; set; }

        /// <summary>
        /// 是否重复，控制统计时间内，多条数据是否计算为多次
        /// </summary>
        public bool IsRepeat { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 项目分类（系统项目）
        /// </summary>
        [StringLength(50)]
        public string ModuleName { get; set; }

        /// <summary>
        /// 参数所属模块
        /// </summary>
        [StringLength(20)]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        [StringLength(20)]
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [StringLength(80)]
        public string ParaName { get; set; }

        /// <summary>
        /// 条件（系统项目），0：小于，1：大于，2：等于，3：小于等于，4：大于等于，5：包含，6：不包含
        /// </summary>
        public int? Condition { get; set; }

        /// <summary>
        /// 内容，多个关键字用英文逗号隔开
        /// </summary>
        [StringLength(250)]
        public string Content { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空！")]
        public int SortNum { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "模块类型不能为空！")]
        public string ModuleType { get; set; }

        /// <summary>
        /// 用于存储床位概览配置项的图标显示编码
        /// </summary>
        [CanBeNull]
        [StringLength(30)]
        public string Icon { get; set; }

        [CanBeNull]
        [StringLength(20)]
        public string DeptCode { get; set; }
    }
}
