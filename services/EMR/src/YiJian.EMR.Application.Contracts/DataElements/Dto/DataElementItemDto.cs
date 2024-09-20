using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.DataElements.Dto
{
    /// <summary>
    /// 数据元素
    /// </summary>
    public class DataElementItemDto:EntityDto<Guid>
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [StringLength(32)]
        public string No { get; set; }

        /// <summary>
        /// 名称
        /// </summary> 
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 背景文本
        /// </summary> 
        [StringLength(50)]
        public string Watermark { get; set; }

        /// <summary>
        /// 提示文本
        /// </summary> 
        [StringLength(50)]
        public string Tips { get; set; }

        /// <summary>
        /// 起始边框
        /// </summary> 
        [StringLength(50)]
        public string BeginMargin { get; set; }

        /// <summary>
        /// 结尾边框
        /// </summary> 
        [StringLength(50)]
        public string EndMargin { get; set; }

        /// <summary>
        /// 固定宽度
        /// </summary> 
        public int FixedWidth { get; set; }

        /// <summary>
        /// 只读状态
        /// </summary> 
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 数据源
        /// </summary> 
        [StringLength(50)]
        public string DataSource { get; set; }

        /// <summary>
        /// 绑定路径
        /// </summary> 
        [StringLength(50)]
        public string BindPath { get; set; }

        /// <summary>
        /// 级联表达式
        /// </summary> 
        [StringLength(100)]
        public string CascadeExpression { get; set; }

        /// <summary>
        /// 数值表达式
        /// </summary> 
        [StringLength(100)]
        public string NumericalExpression { get; set; }

        /// <summary>
        /// 用户可以直接修改内容
        /// </summary> 
        public bool CanModify { get; set; }

        /// <summary>
        /// 允许被删除
        /// </summary> 
        public bool CanDelete { get; set; }

        /// <summary>
        /// 输入域类型
        /// </summary> 
        [StringLength(50)]
        public string InputType { get; set; }

        /// <summary>
        ///  是否开启校验
        /// </summary> 
        public bool NeedVerification { get; set; }

        /// <summary>
        ///  校验表达式
        /// </summary> 
        [StringLength(200)]
        public string VerificationExpression { get; set; }

        /// <summary>
        /// 数据元Id
        /// </summary> 
        public Guid DataElementId { get; set; }
    }
}
