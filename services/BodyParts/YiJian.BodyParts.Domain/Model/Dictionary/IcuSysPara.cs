using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YiJian.BodyParts.Domain.Shared.Enum;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:系统-参数设置表
    /// </summary>
    public class IcuSysPara : Entity<Guid>
    {
        public IcuSysPara() { }

        public IcuSysPara(Guid id) : base(id) { }

        public IcuSysPara(string moduleName,string moduleSort, string paraCode, string paraName, SysTypeEnum type, SysValueTypeEnum valueType, string paraType, string deptCode, string paraValue, string desc,
            string sourceName, bool IsAddiable = false)
        {
            this.Id = Guid.NewGuid();
            this.ModuleName = moduleName;
            this.ModuleSort = moduleSort;
            this.ParaCode = paraCode;
            this.ParaName = paraName;
            this.Type = type;
            this.ValueType = (int)valueType;
            this.ParaType = paraType;
            this.DeptCode = deptCode;
            this.ParaValue = paraValue;
            this.Desc = desc;
            this.DataSource = sourceName;
            this.IsAddiable = IsAddiable;
        }

        /// <summary>
        /// 类型
        /// </summary>
        [StringLength(10)]
        [Required]
        public SysTypeEnum Type { get; set; }

        /// <summary>
        /// 参数类型(S-系统参数，D-科室参数)
        /// </summary>
        [StringLength(10)]
        [Required]
        public string ParaType { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [CanBeNull]
        [StringLength(1000)]
        public string ModuleName { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [StringLength(1000)]
        [Required]
        public string ParaName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [CanBeNull]
        [StringLength(8000)]
        public string ParaValue { get; set; }
        
        /// <summary>
        /// 值类型 1：文本  2：下拉  3：单选  4：表格  6：下拉多选框  7：只读文本  8：开关  9：颜色选择框
        /// </summary>
        public int ValueType { get; set; }
        
        /// <summary>
        /// 数据源
        /// </summary>
        [CanBeNull]
        [StringLength(100)]
        public string DataSource { get; set; }
        
        /// <summary>
        /// 模块排序
        /// </summary>
        public string ModuleSort { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 是否可添加
        /// </summary>
        public bool IsAddiable { get; set; } = false;
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
}
