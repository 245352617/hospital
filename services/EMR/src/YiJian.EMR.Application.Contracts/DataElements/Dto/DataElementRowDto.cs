using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.DataElements.Dto
{
    /// <summary>
    /// 数据元项信息
    /// </summary>
    public class DataElementRowDto:EntityDto<Guid>
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
        /// 输入域类型
        /// </summary> 
        [StringLength(50)]
        public string InputType { get; set; }
         
    }
}
