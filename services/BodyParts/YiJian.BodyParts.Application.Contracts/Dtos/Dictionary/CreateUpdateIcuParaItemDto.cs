using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:护理项目表
    /// </summary>
    public class CreateUpdateIcuParaItemDto : EntityDto<Guid>
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        /// <example></example>
        [Required]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        /// <example></example>
        public string ParaName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        /// <example></example>
        public string DisplayName { get; set; }

        /// <summary>
        /// 评分代码
        /// </summary>
        /// <example></example>
        public string ScoreCode { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>
        /// <example></example>
        public string GroupName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        /// <example></example>
        public string UnitName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        /// <example></example>
        public string ValueType { get; set; }

        /// <summary>
        /// 文本类型
        /// </summary>
        /// <example></example>
        public string Style { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        /// <example></example>
        public string DecimalDigits { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        /// <example></example>
        public string MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        /// <example></example>
        public string MinValue { get; set; }

        /// <summary>
        /// 高值
        /// </summary>
        /// <example></example>
        public string HighValue { get; set; }

        /// <summary>
        /// 低值
        /// </summary>
        /// <example></example>
        public string LowValue { get; set; }

        /// <summary>
        /// 是否预警
        /// </summary>
        /// <example></example>
        public bool? Threshold { get; set; }

        /// <summary>
        /// 关联颜色
        /// </summary>
        /// <example></example>
        public string ColorParaCode { get; set; }

        /// <summary>
        /// 关联颜色名称
        /// </summary>
        public string ColorParaName { get; set; }

        /// <summary>
        /// 关联性状
        /// </summary>
        /// <example></example>
        public string PropertyParaCode { get; set; }

        /// <summary>
        /// 关联性状名称
        /// </summary>
        public string PropertyParaName { get; set; }

        /// <summary>
        /// 设备参数代码
        /// </summary>
        public string DeviceParaCode { get; set; }

        /// <summary>
        /// 设备参数类型
        /// </summary>
        public string DeviceParaType { get; set; }

        /// <summary>
        /// 采集时间点
        /// </summary>
        public string DeviceTimePoint { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <example></example>
        public int SortNum { get; set; }

        public List<CreateUpdateDictDto> createUpdateDictDtos { get; set; }
    }
}
