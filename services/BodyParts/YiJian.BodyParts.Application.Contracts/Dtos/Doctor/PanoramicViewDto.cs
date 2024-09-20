using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 全景视图返回参数
    /// </summary>
    public class PanoramicViewDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 样式:药物-->数值，出入量-->柱状图，其他-->曲线图
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 时间轴
        /// </summary>
        public DateTime[] times { get; set; }

        public List<object> paraItems { get; set; }
    }

    /// <summary>
    /// 分组项目
    /// </summary>
    public class GroupItem
    {
        /// <summary>
        /// 参数代码
        /// </summary>
        public string paracode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string paraname { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unitname { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue2 { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue2 { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue3 { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue3 { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public List<object> values { get; set; } = new List<object>();
    }
}
