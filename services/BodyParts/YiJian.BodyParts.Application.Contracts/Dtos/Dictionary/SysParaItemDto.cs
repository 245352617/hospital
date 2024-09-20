using System.Collections.Generic;
using YiJian.BodyParts.Domain.Shared.Enum;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Dictionary
{

    public class SysParaItemDto
    {
        /// <summary>
        /// 配置项代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数类型 S 系统参数  D科室参数
        /// </summary>
        public string ParaType { get; set; }

        /// <summary>
        /// 配置分类 1：系统配置  2：特护单配置
        /// </summary>
        public SysTypeEnum Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// 值类型 1：文本  2：单选  3：复选  4：字符串数组  5：表格
        /// </summary>
        public int ValueType { get; set; }
        
        /// <summary>
        /// 表格模式是否允许新增
        /// </summary>
        public bool IsAddition { get; set; } = false;
        
        /// <summary>
        /// 表格模式列
        /// </summary>
        public List<IcuSysParaTbColDto> Cols { get; set; }

        /// <summary>
        /// 表格模式值列表
        /// </summary>
        public List<Dictionary<string, string>> ValueList { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        public string SourceName { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }

    public class IcuSysParaTbColDto
    {
        /// <summary>
        /// 列标识
        /// </summary>
        public string ColCode { get; set; }
        
        /// <summary>
        /// 列名
        /// </summary>
        public string ColName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
        
        /// <summary>
        /// 配置列类型
        /// </summary>
        public SysValueTypeEnum ValueType { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
        
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool ValidState { get; set; }
    }
}